using System;
using TMS.Application.Dtos;

namespace TMS.Domain.Interfaces;

public interface ITaskService
{
    Task<CreateTaskResponseDto?> UserCreateTaskAsync(CreateTaskRequestDto request);
    Task<CreateTaskResponseDto?> UserUpdateTaskAsync(UpdateTaskRequestDto request, string claimRole);
    Task<List<CreateTaskResponseDto?>> TaskGetAsync(string OwnerUserId, string claimRole);
}
