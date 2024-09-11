namespace Microservices.Customer.Application.Customer.Dtos;

public class ShippingAddressAddDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public Guid CityId { get; set; }
    
}
