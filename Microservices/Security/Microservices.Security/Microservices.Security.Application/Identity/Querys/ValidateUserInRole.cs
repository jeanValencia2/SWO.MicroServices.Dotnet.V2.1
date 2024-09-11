using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record ValidateUserInRole : IRequest<Result<bool>>
{
    public string? UserName { get; set; }
    public string? RoleName { get; set; }
}

public class ValidateUserInRoleHandler : IRequestHandler<ValidateUserInRole, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ValidateUserInRoleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ValidateUserInRole request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserName);
        Guard.Against.NullOrEmpty(request.RoleName);

        return await _identityService.IsUserInRoleAsync(request.UserName, request.RoleName);
    }
}