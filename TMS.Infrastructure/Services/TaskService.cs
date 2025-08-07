using System;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Dtos;
using TMS.Application.Exceptions;
using TMS.Domain.Entities;
using TMS.Domain.Interfaces;
using TMS.Infrastructure.Data;

namespace TMS.Infrastructure.Services;

public class TaskService(ProjectDbContext _context) : ITaskService
{
    public async Task<CreateTaskResponseDto?> UserCreateTaskAsync(CreateTaskRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new ArgumentException("Description is required.");

        var createTask = new UserTask
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            DueDate = request.DueDate,
            OwnerUserId = request.OwnerUserId,
        };

        _context.UsersTask.Add(createTask);
        await _context.SaveChangesAsync();

        return new CreateTaskResponseDto
        {
            Id = createTask.Id,
            Title = createTask.Title,
            Description = createTask.Description,
            IsCompleted = createTask.IsCompleted,
            CreatedAt = createTask.CreatedAt,
            DueDate = createTask.DueDate,
            OwnerUserId = createTask.OwnerUserId,
        };
    }

    public async Task<CreateTaskResponseDto?> UserUpdateTaskAsync(UpdateTaskRequestDto request, string claimRole)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new ArgumentException("Description is required.");

        var task = await _context.UsersTask.FirstOrDefaultAsync(u => u.Id == request.Id) ?? throw new TaskNotFoundException(request.Id.ToString().ToUpper());

        if (claimRole == "Admin")
        {            
            task.IsCompleted = true;
        }
        else
        {
            if (task.OwnerUserId != request.OwnerUserId)
                throw new TaskUserNotMatchedException(request.Id.ToString(), request.OwnerUserId);

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
        }

        await _context.SaveChangesAsync();

        return new CreateTaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            OwnerUserId = task.OwnerUserId,
        };
    }

    public async Task<List<CreateTaskResponseDto?>> TaskGetAsync(string OwnerUserId, string claimRole)
    {
        //var task = new CreateTaskResponseDto();

        if (claimRole == "Admin")
        {
            var task = await _context.UsersTask.AsNoTracking().Select(u => new CreateTaskResponseDto
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                IsCompleted = u.IsCompleted,
                CreatedAt = u.CreatedAt,
                DueDate = u.DueDate,
                OwnerUserId = u.OwnerUserId,
            }).ToListAsync() ?? throw new TaskGetNotFoundException();

            return task!;
        }

        else
        {
            var task = await _context.UsersTask.AsNoTracking().Where(u => u.OwnerUserId == OwnerUserId).Select(u => new CreateTaskResponseDto
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                IsCompleted = u.IsCompleted,
                CreatedAt = u.CreatedAt,
                DueDate = u.DueDate,
                OwnerUserId = u.OwnerUserId,
            }).ToListAsync() ?? throw new TaskGetNotFoundException();

            return task!;
        }
    }
}
