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
                return BadRequest(new { message = "Something wnt wrong!"});
            }
        }
    }
}
