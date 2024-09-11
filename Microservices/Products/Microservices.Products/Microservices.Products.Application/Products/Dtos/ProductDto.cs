
using AutoMapper;
using Microservices.Products.Application.Products.Commands;
using SWO.Microservices.Dotnet.Shared.Domain;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Products.Application.Products.Dtos;

public class ProductDto : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProduct, Product>();
        }
    }
}
