using Microservices.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Customer.Infrastructure.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Tbl_Cities");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Name).IsRequired();        

        builder.HasOne(o => o.State)
            .WithMany()
            .HasForeignKey(o => o.StateId)
            .IsRequired();

        //builder.Ignore(c => c.State);
    }
}

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("Tbl_States");

        builder.HasKey(o => o.Id);
    }
}
