using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.API.Models;


public class UserLoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserLoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

public class RefreshTokenRequest
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}

public class UserLogoutRequest
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}

public class UserLogoutResponse
{
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
}