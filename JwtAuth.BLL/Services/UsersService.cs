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

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
            return _mapper.Map<List<UserResponseDto>>(allUsers);
        }
    }
}