using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands;

public record class AddUserToRole : IRequest<Result<IdentityResult>>
{
    public string? userName { get; set; }
    public string? roleName { get; set; }
}

public class AddUserToRoleHandler : IRequestHandler<AddUserToRole, Result<IdentityResult>>
{
    private readonly IIdentityService _identityService;

    public AddUserToRoleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<IdentityResult>> Handle(AddUserToRole request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.userName);
        Guard.Against.NullOrEmpty(request.roleName);

        return await _identityService.AddUserToRoleAsync(request.userName, request.roleName);
    }
}
