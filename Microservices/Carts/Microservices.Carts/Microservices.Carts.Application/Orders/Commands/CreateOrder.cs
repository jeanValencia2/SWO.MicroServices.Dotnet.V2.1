using AutoMapper;
using MediatR;
using Microservices.Carts.Application.Common.Interfaces;
using Microservices.Carts.Application.Dtos;
using Microservices.Carts.Domain.Aggregates;
using Microservices.Carts.Domain.Enums;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Carts.Application.Orders.Commands;

public class CreateOrder : IRequest<Result<OrderDto>>
{
    public string? OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public DeliveryDetailsDto? DeliveryDetails { get; set; }
    public PaymentInformationDto? PaymentInformation { get; set; }
    public List<ProductQuantityDto> Products { get; set; } = new List<ProductQuantityDto>();
}

public class CreateOrderHandler : IRequestHandler<CreateOrder, Result<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Order>(request);

        entity.Id = Guid.NewGuid();
        entity.Created = DateTime.Now;
        entity.CreatedBy = "Some User";
        entity.Status = OrderStatus.Created;

        //var response = _mapper.Map<OrderDto>(entity);
        //response.Apply(entity.Id, request);
        //await _context.OrderEventRepository.Save(response, cancellationToken);

        var order = await _context.OrderRepository.AddAsync(entity);
        var response = _mapper.Map<OrderDto>(order);

        return response;


    }
}
