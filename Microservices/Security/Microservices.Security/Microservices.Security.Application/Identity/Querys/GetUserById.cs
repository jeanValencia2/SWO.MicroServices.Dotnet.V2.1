using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetUserById : IRequest<Result<ApplicationUser>>
{
    public string? UserId { get; set; }
}

public class GetUserByIdHandler : IRequestHandler<GetUserById, Result<ApplicationUser>>
{
    private readonly IIdentityService _identityService;

    public GetUserByIdHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationUser>> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.UserId);

        var result = await _identityService.FindUserByIdAsync(request.UserId);

        Guard.Against.Null<ApplicationUser>(result);

        return result!;
    }
}
