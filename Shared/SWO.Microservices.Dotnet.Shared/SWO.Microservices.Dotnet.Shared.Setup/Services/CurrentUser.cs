using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SWO.Microservices.Dotnet.Shared.Setup.Services;

public interface ICurrentUser
{
    string? Id { get; }
    string? UserName { get; }
}

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
}
