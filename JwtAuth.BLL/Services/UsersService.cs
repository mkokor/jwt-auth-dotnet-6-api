using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Interfaces;
using JwtAuth.DAL.Entities;
using JwtAuth.DAL.Interfaces;
using JwtAuth.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        private async Task<User> GetUserByUsername(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsername(username);
            if (user != null)
                return user;
            throw new NullReferenceException("User with provided username does not exist!");
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
            return _mapper.Map<List<UserResponseDto>>(allUsers);
        }

        public async Task RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            await _authenticationService.CheckUsernameAvailability(userRegistrationRequestDto.Username);
            _authenticationService.ValidatePasswordStrength(userRegistrationRequestDto.Password);
            _authenticationService.EncodePlaintextPassword(userRegistrationRequestDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userRegistrationRequestDto);
            user.PsswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _unitOfWork.UserRepository.CreateUser(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserLoginResponseDto> LogInUser(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await GetUserByUsername(userLoginRequestDto.Username);
            _authenticationService.ValidatePasswordHash(userLoginRequestDto.Password, user.PsswordHash, user.PasswordSalt);
            return new UserLoginResponseDto()
            {
                JsonWebToken = _authenticationService.GenerateJwt(user)
            };
        }
    }
}