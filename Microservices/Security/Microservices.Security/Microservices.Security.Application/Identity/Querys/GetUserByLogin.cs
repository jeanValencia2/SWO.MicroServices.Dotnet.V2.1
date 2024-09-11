using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetUserByLogin : IRequest<Result<ApplicationUser>>
{
    public string? loginProvider { get; set; }
    public string? providerKey { get; set; }
}

public class GetUserByLoginHandler : IRequestHandler<GetUserByLogin, Result<ApplicationUser>>
{
    private readonly IIdentityService _identityService;

    public GetUserByLoginHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationUser>> Handle(GetUserByLogin request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.loginProvider);
        Guard.Against.NullOrEmpty(request.providerKey);

        var result = await _identityService.FindUserByLoginAsync(request.loginProvider, request.providerKey);

        Guard.Against.Null<ApplicationUser>(result);

        return result!;
    }
}
