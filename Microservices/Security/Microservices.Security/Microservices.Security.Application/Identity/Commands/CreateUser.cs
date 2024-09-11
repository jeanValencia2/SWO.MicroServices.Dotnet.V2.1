using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using Microservices.Security.Domain.Enums;
using System.Drawing;

namespace Microservices.Security.Application.Identity.Commands;

public record CreateUser : IRequest<Result<IdentityResult>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? LoginProvider { get; set; }
}

public class CreateUserHandler : IRequestHandler<CreateUser, Result<IdentityResult>>
{
    private readonly IIdentityService _identityService;

    public CreateUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<IdentityResult>> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.Email);
        Guard.Against.NullOrEmpty(request.Password);
        Guard.Against.NullOrEmpty(request.ConfirmPassword);
        Guard.Against.NullOrEmpty(request.LoginProvider);
        var isLoginProvider = Enum.IsDefined(typeof(LoginProviders), request.LoginProvider);
        Guard.Against.Default(isLoginProvider);

        return await _identityService.CreateUserAsync(request.Email, request.Password, request.LoginProvider);
    }
}