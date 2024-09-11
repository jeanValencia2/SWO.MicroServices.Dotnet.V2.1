using AutoMapper;
using MediatR;
using Microservices.Carts.Application.Common.Interfaces;
using Microservices.Carts.Application.Dtos;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Carts.Application.Orders.Querys;

public record GetOrders : IRequest<Result<PaginatedList<OrderDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetOrdersHandler : IRequestHandler<GetOrders, Result<PaginatedList<OrderDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrdersHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<OrderDto>>> Handle(GetOrders request, CancellationToken cancellationToken)
    {
        var orders = await _context.OrderRepository.GetAllAsync();
        var mapOrders = _mapper.Map<List<OrderDto>>(orders.ToList());
        var result = PaginatedList<OrderDto>.Create(mapOrders.AsQueryable(), mapOrders.Count(), request.PageNumber, request.PageSize);

        return result;
    }
}
