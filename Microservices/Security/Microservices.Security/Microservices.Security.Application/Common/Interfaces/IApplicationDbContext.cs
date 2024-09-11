using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Security.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationClient> ApplicationClient { get; set; }
    DbSet<Domain.Entities.Menu> Menus { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
