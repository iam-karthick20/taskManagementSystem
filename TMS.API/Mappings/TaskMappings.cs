using System;
using TMS.API.Models;
using TMS.Application.Dtos;

namespace TMS.API.Mappings;

public static class TaskMappings
{
    public static CreateTaskRequestDto CreateTaskRequestM(this CreateTaskRequest request)
    {
        return new CreateTaskRequestDto
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            OwnerUserId = request.OwnerUserId,
        };
    }

    public static UpdateTaskRequestDto UpdateTaskRequestM(this UpdateTaskRequest request)
    {
        return new UpdateTaskRequestDto
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            OwnerUserId = request.OwnerUserId,
        };
    }
}
