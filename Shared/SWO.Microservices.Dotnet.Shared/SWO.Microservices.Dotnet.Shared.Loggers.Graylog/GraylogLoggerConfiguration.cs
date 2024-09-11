using Serilog.Events;

namespace SWO.Microservices.Dotnet.Shared.Loggins.Graylog.Loggers;

public class GraylogLoggerConfiguration : ILoggerConfiguration
{
    public bool Enabled { get; set; } = false;
    public LogEventLevel MinimumLevel { get; set; }
    public string Host { get; set; } = "";
    public int Port { get; set; }
}
