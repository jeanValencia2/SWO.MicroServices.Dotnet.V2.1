using Microservices.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microservices.Security.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer.Domain.Aggregates.Customer> Customers { get; set; }
    DbSet<ShippingAddress> ShippingAddresses { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<State> States { get; set; }

    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
