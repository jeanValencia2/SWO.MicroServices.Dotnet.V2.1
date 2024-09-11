using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microservices.Products.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;
using Microservices.Products.Infrastructure.Data.Configurations;

namespace Microservices.Products.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public DbSet<Product> Products { get ; set ; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);        

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new ProductConfiguration());
    }
}
