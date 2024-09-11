using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Mappings;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Customer.Querys;

public record GetCustomers : IRequest<Result<PaginatedList<CustomerDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCustomersHandler : IRequestHandler<GetCustomers, Result<PaginatedList<CustomerDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CustomerDto>>> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        var customerList = await _context.Customers
            .OrderBy(x => x.Name)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return customerList;
    }
}