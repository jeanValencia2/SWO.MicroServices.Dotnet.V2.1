using Serilog.Events;

namespace SWO.Microservices.Dotnet.Shared.Loggins;

public class ConsoleLoggerConfiguration: ILoggerConfiguration
{
    public bool Enabled { get; set; } = false;
    public LogEventLevel MinimumLevel { get; set; }
}
