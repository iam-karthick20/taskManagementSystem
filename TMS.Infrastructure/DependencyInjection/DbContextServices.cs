using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Infrastructure.Data;

namespace TMS.Infrastructure.DependencyInjection;

public static class DbContextServices
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("UserDatabase")));
            
        return services;
    }
}
