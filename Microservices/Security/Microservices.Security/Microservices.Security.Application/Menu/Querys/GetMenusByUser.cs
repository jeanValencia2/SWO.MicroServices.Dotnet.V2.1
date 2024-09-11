using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Mappings;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Menu.Querys;

public record GetMenusByUser : IRequest<Result<List<MenuDto>>>
{
    public string? Email { get; set; }
    public string? ApplicationName { get; set; }
}

public class GetMenusByUserHandler : IRequestHandler<GetMenusByUser, Result<List<MenuDto>>>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMenusByUserHandler(IIdentityService identityService, IApplicationDbContext context, IMapper mapper)
    {
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuDto>>> Handle(GetMenusByUser request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.ApplicationName);
        Guard.Against.NullOrEmpty(request.Email);
        var user = await _identityService.FindUserByNameAsync(request.Email.ToUpper());
        var application = await _context.ApplicationClient.Where(x => x.Name == request.ApplicationName).FirstOrDefaultAsync(cancellationToken);

        Guard.Against.Null<ApplicationUser>(user);
        Guard.Against.Null<ApplicationClient>(application);
        var rolesUser = await _identityService.GetUserRolesAsync(request.Email);

        var menus = await _context.Menus
            .Where(x => x.ApplicationId == application.Id && x.Roles.Any(y => rolesUser.Contains(y)))
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<MenuDto>>(menus);
    }
}