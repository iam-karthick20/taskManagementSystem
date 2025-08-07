using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Domain.Interfaces;
using TMS.Infrastructure.Services;

namespace TMS.Infrastructure.DependencyInjection;

public static class LoggingInfrastructureService
{
    public static IServiceCollection AddLoggingServices(this IServiceCollection services)
    {
        services.AddScoped<ILoggingService, LoggingService>();

        return services;
    }
}
