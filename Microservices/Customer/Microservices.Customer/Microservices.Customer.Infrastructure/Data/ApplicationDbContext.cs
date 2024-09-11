using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Customer.Domain.Entities;
using Microservices.Customer.Infrastructure.Data.Configurations;

namespace Microservices.Customer.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Domain.Aggregates.Customer> Customers { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<State> States { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);        

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new StateConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ShippingAddressConfiguration());
    }
}
