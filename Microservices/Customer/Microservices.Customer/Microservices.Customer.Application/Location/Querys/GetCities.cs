using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Mappings;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Querys;

public record GetCities : IRequest<Result<PaginatedList<CityDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCitiesHandler : IRequestHandler<GetCities, Result<PaginatedList<CityDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCitiesHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CityDto>>> Handle(GetCities request, CancellationToken cancellationToken)
    {
        var citiesList = await _context.Cities            
            .OrderBy(x => x.Name)
            .Include(c => c.State)
            .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return citiesList;
    }
}