using AutoMapper;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Interfaces;
using JwtAuth.DAL.Interfaces;
using JwtAuth.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Services
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
