using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Mappings;
using TMS.API.Models;
using TMS.Application.Exceptions;
using TMS.Domain.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService, ILoggingService _logger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest request)
        {
            try
            {
                _logger.LogInformation("Initiate Login with Username: {Username}", request.Username);

                // Mapping
                var requestDto = request.UserLoginRequestM();

                var result = await _authService.LoginAsync(requestDto);
                _logger.LogInformation("Login Successful with Username: {Username}", request.Username);
                return Ok(result);
            }

            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (UserPasswordInvalidException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserLoginResponse>> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                _logger.LogInformation("Initiate Login with Refresh Token, UserId: {UserId}", request.UserId);

                // Mapping
                var requestDto = request.RefreshTokenRequestM();

                var result = await _authService.RefreshTokenAsync(requestDto);
                _logger.LogInformation("Token Generated Successful with Refresh Token, UserId: {UserId}", request.UserId);
                return Ok(result);
            }
            catch (RefreshTokenUserNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (RefreshTokenInvalidException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Unauthorized(new { message = ex.Message });
            }
            catch (RefreshTokenExpiredException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Unauthorized(new { message = ex.Message });
            }
            catch (RefreshTokenRovokedException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Unauthorized(new { message = ex.Message });
            }
            

        }

        [HttpPost("logout")]
        public async Task<ActionResult<UserLogoutResponse>> Logout(UserLogoutRequest request)
        {
            try
            {
                _logger.LogInformation("Initiate Logout with UserId: {UserId}", request.UserId);

                // Mapping
                var requestDto = request.UserLogoutRequestM();

                var result = await _authService.LogoutAsync(requestDto);
                _logger.LogInformation("Logout Successfull with UserId: {UserId}", request.UserId);
                return Ok(result);
            }
            catch (RefreshTokenRovokedException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Unauthorized(new { message = ex.Message });
            }
            catch (LogoutInvalidException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
