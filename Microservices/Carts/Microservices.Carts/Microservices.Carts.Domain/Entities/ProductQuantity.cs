using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Carts.Domain.Entities;

public class ProductQuantity : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
}
