using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Querys;

public record GetUserByName : IRequest<Result<ApplicationUser>>
{
    public string? Email { get; set; }
}

public class GetUserByNameHandler : IRequestHandler<GetUserByName, Result<ApplicationUser>>
{
    private readonly IIdentityService _identityService;

    public GetUserByNameHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationUser>> Handle(GetUserByName request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.Email);

        var result = await _identityService.FindUserByNameAsync(request.Email.ToUpper());

        Guard.Against.Null<ApplicationUser>(result);

        return result!;
    }
}
