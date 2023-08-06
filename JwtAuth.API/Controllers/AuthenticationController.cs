using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace JwtAuth.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("registration")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationRequestDto userRegistrationRequestDto)
        {
            try
            {
                await _authenticationService.RegisterUser(userRegistrationRequestDto);
                return Ok(new { Message = "User successfully registered!" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> LogInUser([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            try
            {
                var loginResult = await _authenticationService.LogInUser(userLoginRequestDto);
                return Ok(loginResult);
            }
            catch (NullReferenceException exception)
            {
                return NotFound(new { exception.Message });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpGet("access-token-owner/username"), Authorize]
        public ActionResult GetAccessTokenOwnerUsername()
        {
            // Reading claims values from ClaimsPrincipal object...
            return Ok(new { Username = User?.Identity?.Name });
        }

        [HttpGet("access-token-owner"), Authorize]
        public async Task<ActionResult<UserResponseDto>> GetAccessTokenOwner()
        {
            try
            {
                var user = await _authenticationService.GetAccessTokenOwner();
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpGet("access-token-refresh")]
        public async Task<ActionResult<AccessTokenRefreshResponseDto>> RefreshAccessToken()
        {
            try
            {
                var result = await _authenticationService.RefreshAccessToken();
                return Ok(result);
            }
            catch (AuthenticationException exception)
            {
                return Unauthorized(new { exception.Message });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogOutUser()
        {
            try
            {
                await _authenticationService.LogOutUser();
                return Ok(new { Message = "User successfully logged out!" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }
    }
}