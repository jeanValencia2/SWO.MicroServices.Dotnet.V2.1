using AutoMapper;
using Microservices.Customer.Application.Customer.Commands;
using SWO.Microservices.Dotnet.Shared.Domain;
using DomainAggregates = Microservices.Customer.Domain.Aggregates;

namespace Microservices.Customer.Application.Customer.Dtos;

public class CustomerDto : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsVip { get; set; }

    public List<ShippingAddressDto>? ShippingAddresses { get; set; } = new List<ShippingAddressDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DomainAggregates.Customer, CustomerDto>();
            CreateMap<CreateCustomer, DomainAggregates.Customer>();
        }
    }
}
