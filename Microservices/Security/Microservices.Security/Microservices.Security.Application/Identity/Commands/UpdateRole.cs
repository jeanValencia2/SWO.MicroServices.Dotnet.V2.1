using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Security.Application.Identity.Commands;

public record UpdateRole : IRequest<Result<IdentityResult>>
{
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public Guid ApplicationId { get; set; }
}

public class UpdateRoleHandler : IRequestHandler<UpdateRole, Result<IdentityResult>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public UpdateRoleHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Result<IdentityResult>> Handle(UpdateRole request, CancellationToken cancellationToken)
    {
        var applicationClient = await _context.ApplicationClient.FindAsync(new object[] { request.ApplicationId }, cancellationToken);

        Guard.Against.Null<ApplicationClient>(applicationClient);
        Guard.Against.NullOrEmpty(request.RoleId);
        Guard.Against.NullOrEmpty(request.RoleName);
        Guard.Against.NullOrEmpty(request.Description);

        var role = new ApplicationRole
        {
            Id = request.RoleId,
            Name = $"{applicationClient.Name}.{request.RoleName}",
            Description = request.Description,
            ApplicationId = request.ApplicationId
        };

        return await _identityService.UpdateRoleAsync(role);
    }
}
