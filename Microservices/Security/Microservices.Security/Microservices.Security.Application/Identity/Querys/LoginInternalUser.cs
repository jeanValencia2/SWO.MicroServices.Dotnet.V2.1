using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Security.Application.Identity.Querys;

public record LoginInternalUser : IRequest<Result<TokenResult>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginInternalUserHandler : IRequestHandler<LoginInternalUser, Result<TokenResult>>
{
    private readonly IIdentityService _identityService;

    public LoginInternalUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<TokenResult>> Handle(LoginInternalUser request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.Email);
        Guard.Against.NullOrEmpty(request.Password);

        return await _identityService.LoginInternalUser(request.Email, request.Password);
    }
}
