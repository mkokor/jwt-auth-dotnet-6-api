using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                return BadRequest(new { Message = "Something wnt wrong!"});
            }
        }

        [HttpPost("registration")]
        public async Task<ActionResult> RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            try
            {
                await _usersService.RegisterUser(userRegistrationRequestDto);
                return Ok(new { Message = "User successfully registered!" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> LogInUser(UserLoginRequestDto userLoginRequestDto)
        {
            try
            {
                var loginResult = await _usersService.LogInUser(userLoginRequestDto);
                return Ok(loginResult);
            }
            catch (NullReferenceException exception)
            {
                return BadRequest(new { exception.Message });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }
    }
}