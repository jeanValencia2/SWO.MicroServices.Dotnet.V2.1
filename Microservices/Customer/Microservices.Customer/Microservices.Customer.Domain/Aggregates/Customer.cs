using Ardalis.GuardClauses;
using Microservices.Customer.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Customer.Domain.Aggregates;

public class Customer : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsVip { get; set; }

    [ConcurrencyCheck]
    public List<ShippingAddress>? ShippingAddresses { get; set; } = new List<ShippingAddress>();

    public void AddShippingAddress(ShippingAddress shippingAddress)
    {
        ShippingAddresses!.Add(shippingAddress);
    }

    public void RemoveShippingAddress(ShippingAddress shippingAddress)
    {
        ShippingAddresses!.Remove(shippingAddress);
    }

    public void UpdateShippingAddress(Guid shippingAddressId, ShippingAddress updatedShippingAddress)
    {
        var existingShippingAddress = ShippingAddresses!.FirstOrDefault(sa => sa.Id == shippingAddressId);

        Guard.Against.Null<ShippingAddress>(existingShippingAddress);

        existingShippingAddress.Name = updatedShippingAddress.Name;
        existingShippingAddress.Address = updatedShippingAddress.Address;
        existingShippingAddress.City = updatedShippingAddress.City;
    }
}
