using AutoMapper;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Customer.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Customer.Application.Customer.Dtos;

public class ShippingAddressDto : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Address { get; set; }

    public virtual required CityDto City { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ShippingAddress, ShippingAddressDto>();
        }
    }
}
