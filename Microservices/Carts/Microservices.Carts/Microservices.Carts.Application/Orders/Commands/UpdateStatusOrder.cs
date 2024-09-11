
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Carts.Application.Common.Interfaces;
using Microservices.Carts.Application.Dtos;
using Microservices.Carts.Domain.Aggregates;
using Microservices.Carts.Domain.Enums;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Carts.Application.Orders.Commands;

public record UpdateStatusOrder : IRequest<Result<OrderDto>>
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}

public class UpdateStatusOrderHandler : IRequestHandler<UpdateStatusOrder, Result<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateStatusOrderHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(UpdateStatusOrder request, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderRepository.GetAsync(request.Id);

        Guard.Against.NotFound(request.Id, entity);
                
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Some User";
        entity.Status = request.Status;

        //var response = _mapper.Map<OrderDto>(entity);
        //response.Apply(request);
        //await _context.OrderEventRepository.Save(response, cancellationToken);

        await _context.OrderRepository.UpdateAsync(entity);
        var response = _mapper.Map<OrderDto>(entity);

        return response;
    }
}
