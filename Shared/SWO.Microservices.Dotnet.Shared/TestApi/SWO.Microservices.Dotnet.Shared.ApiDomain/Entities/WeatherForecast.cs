using SWO.Microservices.Dotnet.Shared.Domain;

namespace SWO.Microservices.Dotnet.Shared.ApiDomain.Entities;

public class WeatherForecast : BaseEntity<Guid>
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }
}
