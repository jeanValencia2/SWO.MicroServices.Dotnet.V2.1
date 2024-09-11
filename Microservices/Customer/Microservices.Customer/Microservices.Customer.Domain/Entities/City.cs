using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Customer.Domain.Entities;

public class City : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public Guid StateId { get; set; }

    public required State State { get; set; }
}
