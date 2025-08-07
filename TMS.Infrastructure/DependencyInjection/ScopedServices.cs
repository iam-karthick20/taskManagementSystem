using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Domain.Interfaces;
using TMS.Infrastructure.Services;

namespace TMS.Infrastructure.DependencyInjection;

public static class ScopedServices
{
public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ITaskService, TaskService>();
        
        return services;
    }
}
