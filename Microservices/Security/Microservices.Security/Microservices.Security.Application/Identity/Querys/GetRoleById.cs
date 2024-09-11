using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetRoleById : IRequest<Result<ApplicationRole>>
{
    public string? RoleId { get; set; }
}

public class GetRoleByIdHandler : IRequestHandler<GetRoleById, Result<ApplicationRole>>
{
    private readonly IIdentityService _identityService;

    public GetRoleByIdHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationRole>> Handle(GetRoleById request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.RoleId);

        var result = await _identityService.FindRoleByIdAsync(request.RoleId);

        Guard.Against.Null<ApplicationRole>(result);

        return result!;
    }
}