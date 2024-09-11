using AutoMapper;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Menu.Querys;

public record GetMenus : IRequest<Result<PaginatedList<MenuDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetMenusHandler : IRequestHandler<GetMenus, Result<PaginatedList<MenuDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMenusHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<MenuDto>>> Handle(GetMenus request, CancellationToken cancellationToken)
    {
        var result = await _context.Menus
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);

        var lista = _mapper.Map<List<MenuDto>>(result);

        return PaginatedList<MenuDto>.Create(lista.AsQueryable(), lista.Count(), request.PageNumber, request.PageSize);
    }
}