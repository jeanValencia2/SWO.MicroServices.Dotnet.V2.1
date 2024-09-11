using Serilog.Events;

namespace SWO.Microservices.Dotnet.Shared.Loggins;

public interface ILoggerConfiguration
{
    public bool Enabled { get; set; }
    public LogEventLevel MinimumLevel { get; set; }
}
