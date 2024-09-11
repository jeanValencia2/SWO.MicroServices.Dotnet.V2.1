using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Customer.Domain.Entities;

public class State : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }

}
