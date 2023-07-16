using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Utilities.TokenGenerationService;
using JwtAuth.DAL.Entities;
using JwtAuth.DAL.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerationService _tokenGenerationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ITokenGenerationService tokenGenerationService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenGenerationService = tokenGenerationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region User Registration
        private void EncodePlaintextPassword(string plaintextPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
            }
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


        #region User Login
        private async Task<User> GetUserByUsername(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsername(username);
            if (user != null)
                return user;
            throw new NullReferenceException("User with provided username does not exist!");
        }

        // Function below checks if provided plaintext password matches with provided hashed password (with specific salt)!
        public void ValidatePasswordHash(string plaintextPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
                if (computedPasswordHash.SequenceEqual(passwordHash))
                    return;
                throw new Exception("Password does not match the username!");
            }
        }

        public async Task<UserLoginResponseDto> LogInUser(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await GetUserByUsername(userLoginRequestDto.Username);
            ValidatePasswordHash(userLoginRequestDto.Password, user.PasswordHash, user.PasswordSalt);
            return new UserLoginResponseDto()
            {
                JsonWebToken = _tokenGenerationService.GenerateJwt(user)
            };
        }
        #endregion


        #region JSON Web Token Owner
        private int GetJwtOwnerId()
        {
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new Exception("Something went wrong!");
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public async Task<UserResponseDto> GetJwtOwner()
        {
            var user = await _unitOfWork.UserRepository.GetUserById(GetJwtOwnerId());
            return _mapper.Map<UserResponseDto>(user);
        }
        #endregion
    }
}