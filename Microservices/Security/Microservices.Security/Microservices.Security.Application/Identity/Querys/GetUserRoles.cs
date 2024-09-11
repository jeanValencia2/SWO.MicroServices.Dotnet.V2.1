using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetUserRoles : IRequest<Result<List<string>>>
{
    public string? UserName { get; set; }
}

public class GetUserRolesHandler : IRequestHandler<GetUserRoles, Result<List<string>>>
{
    private readonly IIdentityService _identityService;

    public GetUserRolesHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<List<string>>> Handle(GetUserRoles request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserName);

        var result = await _identityService.GetUserRolesAsync(request.UserName);
        return result.ToList();
    }
}
