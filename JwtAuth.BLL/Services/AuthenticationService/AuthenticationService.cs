using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Utilities.CryptoService;
using JwtAuth.BLL.Utilities.TokenGenerationService;
using JwtAuth.DAL.Entities;
using JwtAuth.DAL.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace JwtAuth.BLL.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerationService _tokenGenerationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ICryptoService _cryptoService;

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, ITokenGenerationService tokenGenerationService, IHttpContextAccessor httpContextAccessor, ICryptoService cryptoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenGenerationService = tokenGenerationService;
            _httpContextAccessor = httpContextAccessor;
            _cryptoService = cryptoService;
        }

        #region UserRegistration
        private void EncodePlaintextPassword(string plaintextPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            _cryptoService.Encrypt(plaintextPassword, out passwordHash, out passwordSalt);
        }

        private void ValidatePasswordStrength(string password)
        {
            // minimum 8 characters
            // minimum one digit
            // minimum one special character
            if (Regex.IsMatch(password, "^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$"))
                return;
            throw new Exception("Password needs to contain minimum of 8 characters, one digit and one special character!");
        }

        private async Task CheckUsernameAvailability(string username)
        {
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
            if (allUsers.FirstOrDefault(user => user.Username.Equals(username)) != null)
                throw new Exception("Provided username is not available!");
        }

        public async Task RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            await CheckUsernameAvailability(userRegistrationRequestDto.Username);
            ValidatePasswordStrength(userRegistrationRequestDto.Password);
            EncodePlaintextPassword(userRegistrationRequestDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userRegistrationRequestDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = "Basic User";
            await _unitOfWork.UserRepository.CreateUser(user);
            await _unitOfWork.SaveAsync();
        }
        #endregion

        #region UserLogin
        private async Task<User> GetUserByUsername(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsername(username);
            if (user != null)
                return user;
            throw new NullReferenceException("User with provided username does not exist!");
        }

        // Function below checks if provided plaintext password matches with provided hashed password (with specific salt)!
        private void ValidatePasswordHash(string plaintextPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            var passwordValid = _cryptoService.Compare(plaintextPassword, passwordHash, passwordSalt);
            if (!passwordValid)
                throw new Exception("Password does not match the username!");
        }

        private async Task<Tuple<RefreshToken, string>> CreateRefreshToken(User user)
        {
            var refreshTokenValue = _tokenGenerationService.GenerateRefreshToken();
            _cryptoService.Encrypt(refreshTokenValue, out byte[] valueHash, out byte[] valueSalt);
            var refreshToken = new RefreshToken
            {
                ValueHash = valueHash,
                ValueSalt = valueSalt,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                OwnerId = user.UserId
            };
            refreshToken = await _unitOfWork.RefreshTokenRepository.CreateRefreshToken(refreshToken);
            await _unitOfWork.SaveAsync();
            return new Tuple<RefreshToken, string>(refreshToken, refreshTokenValue);
        }

        private void SetRefreshTokenInHttpOnlyCookie(Tuple<RefreshToken, string> refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Item1.ExpiresAt,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Item2, cookieOptions); // Adding refresh token in HttpOnly cookie...
        }

        private void RemoveRefreshTokenCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
        }

        private async Task<RefreshToken?> GetRefreshTokenByValue(string value)
        {
            var refreshTokens = await _unitOfWork.RefreshTokenRepository.GetAllRefreshTokens();
            var result = refreshTokens.Find(refreshToken => _cryptoService.Compare(value, refreshToken.ValueHash, refreshToken.ValueSalt));
            if (result == null)
                throw new AuthenticationException("Invalid refresh token!");
            return result;
        }
        
        private string GetRefreshTokenFromCookie()
        {
            var refreshTokenValue = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
            if (refreshTokenValue == null)
                throw new AuthenticationException("Refresh token could not be found!");
            return refreshTokenValue;
        }

        private async Task<RefreshToken> ValidateRefreshToken(string refreshTokenValue)
        {
            var refreshToken = await GetRefreshTokenByValue(refreshTokenValue);
            if (refreshToken.ExpiresAt < DateTime.Now)
            {
                await DeleteRefreshToken(refreshToken);
                throw new AuthenticationException("Refresh token expired!");
            }
            return refreshToken;
        }

        private async Task DeleteRefreshToken(RefreshToken refreshToken)
        {
            _unitOfWork.RefreshTokenRepository.DeleteRefreshToken(refreshToken);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserLoginResponseDto> LogInUser(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await GetUserByUsername(userLoginRequestDto.Username);
            ValidatePasswordHash(userLoginRequestDto.Password, user.PasswordHash, user.PasswordSalt);
            SetRefreshTokenInHttpOnlyCookie(await CreateRefreshToken(user));
            return new UserLoginResponseDto
            {
                AccessToken = _tokenGenerationService.GenerateAccessToken(user)
            };
        }

        public async Task<AccessTokenRefreshResponseDto> RefreshAccessToken()
        {
            var refreshToken = await ValidateRefreshToken(GetRefreshTokenFromCookie());
            await DeleteRefreshToken(refreshToken);
            SetRefreshTokenInHttpOnlyCookie(await CreateRefreshToken(refreshToken.Owner));
            return new AccessTokenRefreshResponseDto
            {
                AccessToken = _tokenGenerationService.GenerateAccessToken(refreshToken.Owner)
            };
        }

        public async Task LogOutUser()
        {
            try
            {
                var refreshToken = await ValidateRefreshToken(GetRefreshTokenFromCookie());
                await DeleteRefreshToken(refreshToken);
                RemoveRefreshTokenCookie();
                // Access token should be deleted on client side (possibly from local storage).
            }
            catch (Exception)
            {
                RemoveRefreshTokenCookie();
            }
        }
        #endregion

        #region AccessTokenOwner
        private string GetAccessTokenOwnerNameIdentifier()
        {
            var accessTokenOwnerNameIdentifier = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (accessTokenOwnerNameIdentifier == null)
                throw new AuthenticationException("User is not authenticated!");
            return accessTokenOwnerNameIdentifier.Value;
        }

        private int GetAccessTokenOwnerId()
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new Exception("Something went wrong!");
            return int.Parse(GetAccessTokenOwnerNameIdentifier());
        }

        public async Task<UserResponseDto> GetAccessTokenOwner()
        {
            var user = await _unitOfWork.UserRepository.GetUserById(GetAccessTokenOwnerId());
            return _mapper.Map<UserResponseDto>(user);
        }
        #endregion
    }
}