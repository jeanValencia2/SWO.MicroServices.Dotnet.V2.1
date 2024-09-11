using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Products.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }

    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
