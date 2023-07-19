using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;

namespace JwtAuth.BLL.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto);

        Task<UserLoginResponseDto> LogInUser(UserLoginRequestDto userLoginRequestDto);

        Task<UserResponseDto> GetJwtOwner();

        Task<JwtRefreshResponseDto> RefreshJwt();
    }
}