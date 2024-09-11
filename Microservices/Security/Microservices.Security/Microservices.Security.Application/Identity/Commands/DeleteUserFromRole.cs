using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands;

public record class DeleteUserFromRole : IRequest<Result<IdentityResult>>
{
    public string? UserName { get; set; }
    public string? RoleName { get; set; }
}

public class DeleteUserFromRoleHandler : IRequestHandler<DeleteUserFromRole, Result<IdentityResult>>
{
    private readonly IIdentityService _identityService;

    public DeleteUserFromRoleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<IdentityResult>> Handle(DeleteUserFromRole request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserName);
        Guard.Against.NullOrEmpty(request.RoleName);

        return await _identityService.RemoveUserFromRoleAsync(request.UserName, request.RoleName);
    }
}
