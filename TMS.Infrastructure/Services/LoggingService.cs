using System;
using Microsoft.Extensions.Logging;
using TMS.Domain.Interfaces;

namespace TMS.Infrastructure.Services;

public class LoggingService(ILogger<LoggingService> logger) : ILoggingService
{
    private readonly ILogger<LoggingService> _logger = logger;

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogError(Exception exception, string message, params object[] args)
    {
        _logger.LogError("[{Time}] {Message} | Error: {ErrorMessage}", DateTime.UtcNow, message, exception.Message);
    }
}
