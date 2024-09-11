
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Products.Application.Common.Interfaces;
using Microservices.Products.Application.Products.Dtos;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Mappings;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Products.Application.Products.Querys;

public  record GetProducts : IRequest<Result<PaginatedList<ProductDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductsHandler : IRequestHandler<GetProducts, Result<PaginatedList<ProductDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<ProductDto>>> Handle(GetProducts request, CancellationToken cancellationToken)
    {
        var productsList = await _context.Products
            .OrderBy(x => x.Name)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return productsList;
    }
}
