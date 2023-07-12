using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            try
            {
                var allUsers = await _usersService.GetAllUsers();
                return Ok(allUsers);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Something wnt wrong!"});
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            try
            {
                await _usersService.RegisterUser(userRegistrationRequestDto);
                return Ok(new { Message = "User successfully registered!" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { Message = exception.Message });
            }
        }
    }
}