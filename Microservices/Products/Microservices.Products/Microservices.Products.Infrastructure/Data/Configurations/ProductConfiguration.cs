using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Products.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Tbl_Products");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Description).IsRequired();
        builder.Property(o => o.Stock).IsRequired();
        builder.Property(o => o.Price).IsRequired();
    }
}
