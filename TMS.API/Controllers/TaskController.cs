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
    public class TaskController(ITaskService _taskService, ILoggingService _logger) : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost("")]
        public async Task<ActionResult<CreateTaskResponse>> TaskCreate(CreateTaskRequest request)
        {
            try
            {
                _logger.LogInformation("Creating new Task with User: {OwnerUserId}", request.OwnerUserId);

                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                request.OwnerUserId = claimUserId!.ToUpper();

                //Mapping
                var requestDto = request.CreateTaskRequestM();

                var res = await _taskService.UserCreateTaskAsync(requestDto);

                _logger.LogInformation("Task Created by User: {OwnerUserId} successfully", res!.OwnerUserId);
                return StatusCode(StatusCodes.Status201Created, res);
            }

            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("")]
        public async Task<ActionResult<CreateTaskResponse>> TaskUpdate(UpdateTaskRequest request)
        {
            try
            {
                _logger.LogInformation("Updating Task : {Id} with User: {OwnerUserId}", request.Id, request.OwnerUserId);

                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;

                request.OwnerUserId = claimUserId!.ToUpper();

                //Mapping
                var requestDto = request.UpdateTaskRequestM();

                var res = await _taskService.UserUpdateTaskAsync(requestDto, claimRole!);

                _logger.LogInformation("Task: {Id} Updated by User: {OwnerUserId} successfully", res!.Id, res!.OwnerUserId);
                return StatusCode(StatusCodes.Status201Created, res);
            }

            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (TaskNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Conflict(new { message = ex.Message });
            }
            catch (TaskUserNotMatchedException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Conflict(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<CreateTaskResponse>> TaskGet()
        {
            try
            {
                _logger.LogInformation("Get Task Initiated");

                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
                
                var res = await _taskService.TaskGetAsync(claimUserId!, claimRole!);

                _logger.LogInformation("Task Got successfully by User {claimUserId}", claimUserId!);
                return StatusCode(StatusCodes.Status200OK, res);
            }

            catch (TaskGetNotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
