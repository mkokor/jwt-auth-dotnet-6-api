using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            try
            {
                var allUsers = await _usersService.GetAllUsers();
                return Ok(allUsers);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something went wrong!"});
            }
        }
    }
}