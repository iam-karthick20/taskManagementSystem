using System;
using Serilog;
using Serilog.Events;

namespace TMS.API.Logging_SeriLog;

public static class LoggingConfiguration
{
    public static void ConfigureLogger(IConfiguration configuration)
    {
        var generalPath = configuration.GetValue<string>("LoggingConfigration:GeneralLogFilePath");
        var errorPath = configuration.GetValue<string>("LoggingConfigration:ErrorLogFilePath");
        var outputTemplate = configuration.GetValue<string>("LoggingConfigration:OutputTemplate");
        var rollingString = configuration.GetValue<string>("LoggingConfigration:RollingInterval");

        // Rolling Interval value passed through config - else its day
        var rollingIntervalConfig = Enum.TryParse<RollingInterval>(rollingString, out var interval)
            ? interval
            : RollingInterval.Day;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)

            .WriteTo.Console()

            // Info and Warning to general-log.txt
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information || evt.Level == LogEventLevel.Warning)
                .WriteTo.File(generalPath!, rollingInterval: rollingIntervalConfig,
                    outputTemplate: outputTemplate!)
            )

            // Error and above to error-log.txt
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(evt => evt.Level >= LogEventLevel.Error)
                .WriteTo.File(errorPath!, rollingInterval: rollingIntervalConfig,
                    outputTemplate: outputTemplate!)
            )

            .CreateLogger();
    }
}
