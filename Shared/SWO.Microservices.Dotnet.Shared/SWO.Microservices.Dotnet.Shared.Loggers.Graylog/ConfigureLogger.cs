using SWO.Microservices.Dotnet.Shared.Loggins.Graylog.Loggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SWO.Microservices.Dotnet.Shared.Discovery;

namespace SWO.Microservices.Dotnet.Shared.Loggins.Graylog;

public static class ConfigureLogger
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, IServiceDiscovery discovery)
        => builder.UseSerilog((context, loggerConfiguration)
            => ConfigureSerilogLogger(loggerConfiguration, context.Configuration, discovery));

    private static LoggerConfiguration ConfigureSerilogLogger(LoggerConfiguration loggerConfiguration, IConfiguration configuration, IServiceDiscovery discovery)
    {
        var graylogLogger = new GraylogLoggerConfiguration();
        configuration.GetSection("Logging:Graylog").Bind(graylogLogger);
        DiscoveryData discoveryData = discovery.GetDiscoveryData(DiscoveryServices.Graylog).Result;
        graylogLogger.Host = discoveryData.Server;
        graylogLogger.Port = discoveryData.Port;
        ConsoleLoggerConfiguration consoleLogger = new ConsoleLoggerConfiguration();
        configuration.GetSection("Logging:Console").Bind(consoleLogger);

        return loggerConfiguration
                .AddConsoleLogger(consoleLogger)
                .AddServiceLogger(graylogLogger);
    }
}