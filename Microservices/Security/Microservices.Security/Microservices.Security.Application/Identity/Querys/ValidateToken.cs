using IdentityModel.OidcClient;
using MediatR;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Security.Claims;

namespace Microservices.Security.Application.Identity.Querys;

public record ValidateToken : IRequest<Result<bool>>
{
    public string Token { get; set; }
}

public class ValidateTokenHandler : IRequestHandler<ValidateToken, Result<bool>>
{
    private readonly IServiceToken _serviceToken;

    public ValidateTokenHandler(IServiceToken serviceToken)
    {
        _serviceToken = serviceToken;
    }

    public async Task<Result<bool>> Handle(ValidateToken request, CancellationToken cancellationToken)
    {
        return await _serviceToken.ValidateToken(request.Token);
    }
}
