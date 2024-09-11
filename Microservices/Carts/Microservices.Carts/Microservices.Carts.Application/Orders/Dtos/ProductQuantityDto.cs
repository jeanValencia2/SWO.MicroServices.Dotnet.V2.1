using AutoMapper;
using Microservices.Carts.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Carts.Application.Dtos;
public class ProductQuantityDto : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductQuantity, ProductQuantityDto>();
            CreateMap<ProductQuantityDto, ProductQuantity>();
        }
    }
}
