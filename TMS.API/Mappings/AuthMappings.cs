using System;
using TMS.API.Models;
using TMS.Application.Dtos;

namespace TMS.API.Mappings;

public static class AuthMappings
{
    public static UserLoginRequestDto UserLoginRequestM(this UserLoginRequest request)
    {
        return new UserLoginRequestDto
        {
            Username = request.Username,
            Password = request.Password,
        };
    }

    public static RefreshTokenRequestDto RefreshTokenRequestM(this RefreshTokenRequest request)
    {
        return new RefreshTokenRequestDto
        {
            UserId = request.UserId,
            RefreshToken = request.RefreshToken,
        };
    }

    public static UserLogoutRequestDto UserLogoutRequestM(this UserLogoutRequest request)
    {
        return new UserLogoutRequestDto
        {
            UserId = request.UserId,
            RefreshToken = request.RefreshToken,
        };
    }
}
