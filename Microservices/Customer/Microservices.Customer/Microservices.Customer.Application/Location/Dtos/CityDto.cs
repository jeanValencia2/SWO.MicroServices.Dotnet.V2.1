using AutoMapper;
using Microservices.Customer.Application.Location.Commands;
using Microservices.Customer.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Customer.Application.Location.Dtos;

public class CityDto : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public Guid StateId { get; set; }

    public virtual required StateDto State { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<City, CityDto>();
            CreateMap<CreateCity, City>();
        }
    }
}
