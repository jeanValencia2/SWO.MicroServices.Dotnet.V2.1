using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Security.Infrastructure.Data.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Tbl_Menus");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Roles)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

    }
}
