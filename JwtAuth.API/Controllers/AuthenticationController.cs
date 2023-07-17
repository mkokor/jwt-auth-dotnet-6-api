using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.BLL.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Authentication;
using System.Security.Claims;

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
        public async Task<ActionResult> RegisterUser(UserRegistrationRequestDto userRegistrationRequestDto)
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
        public async Task<ActionResult<UserLoginResponseDto>> LogInUser(UserLoginRequestDto userLoginRequestDto)
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

        [HttpGet("jwt-owner/username"), Authorize]
        public ActionResult GetJwtOwnerUsername()
        {
            // Reading claims values from ClaimsPrincipal object...
            return Ok(new { Username = User?.Identity?.Name });
        }

        [HttpGet("jwt-owner"), Authorize]
        public async Task<ActionResult<UserResponseDto>> GetJwtOwner()
        {
            try
            {
                var user = await _authenticationService.GetJwtOwner();
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
        }

        [HttpGet("jwt-refresh")]
        public async Task<ActionResult<JwtRefreshResponseDto>> RefreshJwt()
        {
            try
            {
                var result = await _authenticationService.RefreshJwt();
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
    }
}
