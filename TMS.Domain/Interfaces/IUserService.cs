
using TMS.Application.Dtos;

namespace TMS.Domain.Interfaces;

public interface IUserService
{
    Task<UserCreateResponseDto?> UserCreateAsync(UserCreateRequestDto request);
    Task<List<UserGetAllResponseDto?>> UserGetAllAsync();
    Task<UsersGetResponseDto?> UserGetAsync(Guid userId);
} 