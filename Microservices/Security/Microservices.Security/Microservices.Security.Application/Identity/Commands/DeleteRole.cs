using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands;

public record DeleteRole : IRequest<Result<IdentityResult>>
{
    public string? RoleId { get; set; }
}

public class DeleteRoleHandler : IRequestHandler<DeleteRole, Result<IdentityResult>>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<IdentityResult>> Handle(DeleteRole request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.RoleId);

        return await _identityService.DeleteRoleAsync(request.RoleId);
    }
}
