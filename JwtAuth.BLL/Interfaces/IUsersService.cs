using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<List<UserResponseDto>> GetAllUsers();
        Task RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto);
    }
}