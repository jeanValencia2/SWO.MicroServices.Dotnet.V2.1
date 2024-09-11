using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Mappings;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Querys;

public  record GetStates : IRequest<Result<PaginatedList<StateDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetStatesHandler : IRequestHandler<GetStates, Result<PaginatedList<StateDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStatesHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<StateDto>>> Handle(GetStates request, CancellationToken cancellationToken)
    {
        var stateslist = await _context.States
            .OrderBy(x => x.Name)
            .ProjectTo<StateDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return stateslist;
    }
}