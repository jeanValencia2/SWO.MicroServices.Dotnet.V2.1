using AutoMapper;
using Microservices.Carts.Domain.ValueObjects;

namespace Microservices.Carts.Application.Dtos;
public class DeliveryDetailsDto
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DeliveryDetails, DeliveryDetailsDto>();
            CreateMap<DeliveryDetailsDto, DeliveryDetails>();
        }
    }
}
