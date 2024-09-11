using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace SWO.Microservices.Dotnet.Shared.Loggins.Graylog.Loggers;

public static class LoggerConfigurationExtensions
{

    public static LoggerConfiguration AddConsoleLogger(this LoggerConfiguration loggerConfiguration, ConsoleLoggerConfiguration consoleLoggerConfiguration)
    {
        return consoleLoggerConfiguration.Enabled
            ? loggerConfiguration.WriteTo.Console(consoleLoggerConfiguration.MinimumLevel)
            : loggerConfiguration;
    }

    public static LoggerConfiguration AddServiceLogger(this LoggerConfiguration loggerConfiguration, GraylogLoggerConfiguration graylogLoggerConfiguration)
    {
         return graylogLoggerConfiguration.Enabled
             ? loggerConfiguration.WriteTo.Graylog(graylogLoggerConfiguration.Host, graylogLoggerConfiguration.Port, TransportType.Udp, minimumLogEventLevel: graylogLoggerConfiguration.MinimumLevel)
             : loggerConfiguration;
    }
}
