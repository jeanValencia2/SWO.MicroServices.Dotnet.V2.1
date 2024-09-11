using Microsoft.AspNetCore.Identity;

namespace Microservices.Security.Domain.Entities;

public  class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }
    public Guid ApplicationId { get; set; }
}
