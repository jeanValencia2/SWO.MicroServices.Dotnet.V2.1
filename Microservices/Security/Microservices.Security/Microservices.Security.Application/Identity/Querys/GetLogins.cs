using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record class GetLogins : IRequest<Result<List<UserLoginInfo>>>
{
    public string? UserId { get; set; }
}

public class GetLoginsHandler : IRequestHandler<GetLogins, Result<List<UserLoginInfo>>>
{
    private readonly IIdentityService _identityService;

    public GetLoginsHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<List<UserLoginInfo>>> Handle(GetLogins request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserId);

        var result = await _identityService.GetLoginsAsync(request.UserId);
        return result.ToList();
    }
}
