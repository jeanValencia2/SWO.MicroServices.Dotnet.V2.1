namespace SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;

public class ErrorDto
{
    public string? Message { get; set; }
    public Guid? ErrorCode { get; set; }
}
