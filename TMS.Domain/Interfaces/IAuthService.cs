using System;
using TMS.Application.Dtos;

namespace TMS.Domain.Interfaces;

public interface IAuthService
{
    Task<UserLoginResponseDto?> LoginAsync(UserLoginRequestDto request);
    Task<UserLoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<UserLogoutResponseDto?> LogoutAsync(UserLogoutRequestDto request); 
}
