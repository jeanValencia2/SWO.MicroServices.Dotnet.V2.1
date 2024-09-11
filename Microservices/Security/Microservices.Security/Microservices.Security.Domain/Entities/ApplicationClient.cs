using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Security.Domain.Entities;

public class ApplicationClient : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ReturnUrl { get; set; }
}
