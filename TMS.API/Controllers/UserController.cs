using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Mappings;
using TMS.API.Models;
using TMS.Application.Exceptions;
using TMS.Domain.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService, ILoggingService _logger) : ControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<UserCreateResponse>> UserCreate(UserCreateRequest request)
        {
            try
            {
                _logger.LogInformation("Creating new user with Username: {Username}, Email: {EmailAddress}", request.Username, request.EmailAddress);

                //Mapping
                var requestDto = request.UserCreateRequestM();

                var user = await _userService.UserCreateAsync(requestDto);

                _logger.LogInformation("User created successfully: {UserId}", user!.UserId);
                return StatusCode(StatusCodes.Status201Created, user);
            }

            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (DuplicateUserException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Conflict(new { message = ex.Message }); // HTTP 409
            }
            catch (DuplicateUserEmailException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Conflict(new { message = ex.Message }); // HTTP 409
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<ActionResult<List<UsersGetResponse>>> UserGetAll()
        {
            try
            {
                _logger.LogInformation("Initiating user getting all");
                var users = await _userService.UserGetAllAsync();

                _logger.LogInformation("User get all fetched successfully");
                return Ok(users);
            }
            catch (UsersGetNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return NotFound(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UsersGetResponse>>> UserGet(Guid userId)
        {
            try
            {
                _logger.LogInformation("Initiating user: {userId} getting all", userId);

                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(claimUserId) || claimUserId != userId.ToString())
                {
                    _logger.LogWarning("Forbidden access attempt by user: {claimUserId} to access userId: {userId}", claimUserId!, userId);
                    return Forbid(); 
                }

                var users = await _userService.UserGetAsync(userId);

                _logger.LogInformation("User: {userId} get fetched successfully", userId);
                return Ok(users);
            }
            catch (UserGetNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return NotFound(new { message = ex.Message });
            }

        }

    }


}
