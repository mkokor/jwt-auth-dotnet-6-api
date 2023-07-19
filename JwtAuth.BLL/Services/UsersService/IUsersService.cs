using JwtAuth.BLL.DTOs.Responses;

namespace JwtAuth.BLL.Services.UsersService
{
    public interface IUsersService
    {
        Task<List<UserResponseDto>> GetAllUsers();
    }
}