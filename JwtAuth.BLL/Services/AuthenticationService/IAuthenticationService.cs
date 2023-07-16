using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto);

        Task<UserLoginResponseDto> LogInUser(UserLoginRequestDto userLoginRequestDto);
    }
}
