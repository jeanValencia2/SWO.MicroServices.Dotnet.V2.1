using AutoMapper;
using Microservices.Carts.Application.Orders.Commands;
using Microservices.Carts.Domain.Aggregates;
using Microservices.Carts.Domain.Enums;
using SWO.Microservices.Dotnet.Shared.EventSourcing;

namespace Microservices.Carts.Application.Dtos;


public class OrderDto : Aggregate, IApply<Guid, CreateOrder>
{
    protected OrderDto(Guid id) : base(id)
    {
    }

    public Guid Id { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    public DeliveryDetailsDto? DeliveryDetails { get; set; }
    public PaymentInformationDto? PaymentInformation { get; set; }
    
    public List<ProductQuantityDto>? Products { get; set; }

    public void Apply(Guid id, CreateOrder ev)
    {
        Id = id;
        OrderNumber = ev.OrderNumber;
        OrderDate = ev.OrderDate;
        Status = OrderStatus.Created;
        DeliveryDetails = ev.DeliveryDetails;
        PaymentInformation = ev.PaymentInformation;
        Products = ev.Products;
        ApplyChange(ev);
    }

    public void Apply(UpdateStatusOrder ev)
    {
        Status = ev.Status;
        ApplyChange(ev);
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrder, Order>();
        }
    }
}
