using Microservices.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aggregates = Microservices.Customer.Domain.Aggregates;

namespace Microservices.Customer.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Customer> builder)
    {
        builder.ToTable("Tbl_Customers");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Email).IsRequired();
        builder.Property(o => o.RegistrationDate).IsRequired();
        builder.Property(o => o.IsVip).IsRequired();

        builder.HasMany(o => o.ShippingAddresses)
            .WithOne()
            .HasForeignKey(pq => pq.CustomerId);
    }
}

public class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
{
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.ToTable("Tbl_ShippingAddresses");
        builder.HasKey(o => o.Id);

        builder.HasOne<Aggregates.Customer>()  // Uno ShippingAddress pertenece a un Customer
            .WithMany(c => c.ShippingAddresses)  // Un Customer puede tener muchas ShippingAddresses
            .HasForeignKey(sa => sa.CustomerId);

        builder.HasOne(sa => sa.City)
            .WithMany()
            .IsRequired();
    }
}