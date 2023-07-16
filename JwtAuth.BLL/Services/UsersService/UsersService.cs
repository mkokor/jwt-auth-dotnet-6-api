using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Services.AuthenticationService;
using JwtAuth.DAL.Entities;
using JwtAuth.DAL.Repositories;
using JwtAuth.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
            return _mapper.Map<List<UserResponseDto>>(allUsers);
        }
    }
}