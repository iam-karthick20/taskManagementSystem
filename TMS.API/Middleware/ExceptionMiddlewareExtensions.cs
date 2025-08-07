
using Microsoft.AspNetCore.Diagnostics;
using TMS.Domain.Interfaces;

namespace TMS.API.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is not null)
                {
                    using var scope = scopeFactory.CreateScope();
                    var logger = scope.ServiceProvider.GetRequiredService<ILoggingService>();

                    logger.LogError(contextFeature.Error, "Unhandled exception occurred");

                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error"
                    });
                }
            });
        });
    }
}
