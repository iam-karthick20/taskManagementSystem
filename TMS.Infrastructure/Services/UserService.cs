using System;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Dtos;
using TMS.Application.Exceptions;
using TMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using TMS.Infrastructure.Data;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Services;

public class UserService(ProjectDbContext _context) : IUserService
{
    public async Task<UserCreateResponseDto?> UserCreateAsync(UserCreateRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Username and password are required.");

        // Check if username already exists
        var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == request.Username);
        if (existingUser is not null)
            throw new DuplicateUserException(request.Username);

        // Check if Email already exists
        var existingUserEmail = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EmailAddress == request.EmailAddress);
        if (existingUserEmail is not null)
            throw new DuplicateUserEmailException(request.EmailAddress);

        // Create new user
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role, // optional default
            EmailAddress = request.EmailAddress,
            IsActive = true,
            CreatedOn = DateTime.Now,
        };

        // Secure password hashing
        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);


        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserCreateResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Role = user.Role,
            EmailAddress = user.EmailAddress,
            CreatedOn = user.CreatedOn
        };
    }

    public async Task<List<UserGetAllResponseDto?>> UserGetAllAsync()
    {
        var user = await _context.Users.AsNoTracking().Select(u => new UserGetAllResponseDto
        {
            UserId = u.UserId.ToString(),
            Username = u.Username!,
            EmailAddress = u.EmailAddress!,
            Role = u.Role!,
            IsActive = u.IsActive,
            CreatedOn = u.CreatedOn,

        }).ToListAsync() ?? throw new UsersGetNotFoundException();

        return user!;
    }

    // User Get by Guid UserID
    public async Task<UsersGetResponseDto?> UserGetAsync(Guid userId)
    {
        var user = await _context.Users
        .AsNoTracking()
        .Where(u => u.UserId == userId)
        .Select(u => new UsersGetResponseDto
        {
            UserId = u.UserId,
            Username = u.Username!,
            EmailAddress = u.EmailAddress!,
            Role = u.Role!,
            IsActive = u.IsActive,
            CreatedOn = u.CreatedOn,
        })
        .FirstOrDefaultAsync() ?? throw new UserGetNotFoundException(userId);

        return user; 
    }
    
}
