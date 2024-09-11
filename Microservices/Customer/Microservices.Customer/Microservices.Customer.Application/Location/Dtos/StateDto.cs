using AutoMapper;
using Microservices.Customer.Application.Location.Commands;
using Microservices.Customer.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Customer.Application.Location.Dtos;

public class StateDto : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<State, StateDto>();
            CreateMap<CreateState, State>();
        }
    }

}
