using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Application.Identity.Querys;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands;

public record AuthorizeUserInPolicy : IRequest<Result<bool>>
{
    public string? UserId { get; set; }
    public string? PolicyName { get; set; }
}

public class AuthorizeUserInPolicyHandler : IRequestHandler<AuthorizeUserInPolicy, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public AuthorizeUserInPolicyHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(AuthorizeUserInPolicy request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserId);
        Guard.Against.NullOrEmpty(request.PolicyName);

        return await _identityService.AuthorizeAsync(request.UserId, request.PolicyName);
    }
}
