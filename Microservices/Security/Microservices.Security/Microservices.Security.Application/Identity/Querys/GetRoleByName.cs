using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetRoleByName : IRequest<Result<ApplicationRole>>
{
    public string? RoleName { get; set; }
}

public class GetRoleByNameHandler : IRequestHandler<GetRoleByName, Result<ApplicationRole>>
{
    private readonly IIdentityService _identityService;

    public GetRoleByNameHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationRole>> Handle(GetRoleByName request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.RoleName);

        var result = await _identityService.FindRoleByNameAsync(request.RoleName);

        Guard.Against.Null<ApplicationRole>(result);

        return result!;
    }
}