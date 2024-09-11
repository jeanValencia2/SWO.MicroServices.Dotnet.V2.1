using AutoMapper;
using Microservices.Security.Application.Identity.Commands;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Security.Application.Identity.Dtos;

public class ApplicationClientDto : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ReturnUrl { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationClient, ApplicationClientDto>();
            CreateMap<CreateApplicationClient, ApplicationClient>();
        }
    }
}
