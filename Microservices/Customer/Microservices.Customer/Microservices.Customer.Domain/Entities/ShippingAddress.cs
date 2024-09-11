using SWO.Microservices.Dotnet.Shared.Domain;
using Aggregates = Microservices.Customer.Domain.Aggregates;

namespace Microservices.Customer.Domain.Entities;

public class ShippingAddress : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Address { get; set; }

    public virtual required City City { get; set; }

    public Guid CustomerId { get; set; }

    //public required Aggregates.Customer Customer { get; set; }

}
