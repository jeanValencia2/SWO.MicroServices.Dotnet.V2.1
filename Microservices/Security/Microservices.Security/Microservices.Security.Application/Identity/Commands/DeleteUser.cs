using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands
{
    public record DeleteUser : IRequest<Result<IdentityResult>>
    {
        public string? UserId { get; set; }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUser, Result<IdentityResult>>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<IdentityResult>> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.UserId);

            return await _identityService.DeleteUserAsync(request.UserId);
        }
    }
}
