using System;
using TMS.API.Models;
using TMS.Application.Dtos;

namespace TMS.API.Mappings;

public static class UserMappings
{
    public static UserCreateRequestDto UserCreateRequestM(this UserCreateRequest request)
    {
        return new UserCreateRequestDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Password = request.Password,
            Role = request.Role,
            EmailAddress = request.EmailAddress,
        };
    }

    public static UserUpdateRequestDto UserUpdateRequestM(this UserUpdateRequest request)
    {
        return new UserUpdateRequestDto
        {
            UserId = request.UserId,
            Username = request.Username,
            Password = request.Password,
            Role = request.Role,
            EmailAddress = request.EmailAddress,
            IsActive = request.IsActive,
            ModifiedBy = request.ModifiedBy,
        };
    }
}
