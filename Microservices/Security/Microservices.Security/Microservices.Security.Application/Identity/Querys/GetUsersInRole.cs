using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetUsersInRole : IRequest<Result<List<ApplicationUser>>>
{
    public string? RoleName { get; set; }
}

public class GetUsersInRoleHandler : IRequestHandler<GetUsersInRole, Result<List<ApplicationUser>>>
{
    private readonly IIdentityService _identityService;

    public GetUsersInRoleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<List<ApplicationUser>>> Handle(GetUsersInRole request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.RoleName);

        var result = await _identityService.GetUsersInRoleAsync(request.RoleName);
        return result.ToList();
    }
}
