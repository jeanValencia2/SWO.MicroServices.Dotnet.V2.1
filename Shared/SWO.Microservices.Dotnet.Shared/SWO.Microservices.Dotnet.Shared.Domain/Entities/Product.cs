namespace SWO.Microservices.Dotnet.Shared.Domain.Entities;

public class Product :BaseAuditableEntity<Guid>
{   
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}
