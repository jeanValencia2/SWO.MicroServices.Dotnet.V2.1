using Microservices.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Security.Infrastructure.Data.Configurations;

public class ApplicationClientConfiguration : IEntityTypeConfiguration<ApplicationClient>
{
    public void Configure(EntityTypeBuilder<ApplicationClient> builder)
    {
        builder.ToTable("Tbl_Clients");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.ReturnUrl).IsRequired();
    }
}
