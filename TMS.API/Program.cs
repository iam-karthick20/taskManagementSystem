
using Scalar.AspNetCore;
using Serilog;
using TMS.API.Logging_SeriLog;
using TMS.API.Middleware;
using TMS.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContextServices(builder.Configuration);
builder.Services.AddScopedServices();

builder.Services.AddJWTServices(builder.Configuration);
builder.Services.AddLoggingServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Logging Configration from a static class
LoggingConfiguration.ConfigureLogger(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseGlobalExceptionHandler();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
