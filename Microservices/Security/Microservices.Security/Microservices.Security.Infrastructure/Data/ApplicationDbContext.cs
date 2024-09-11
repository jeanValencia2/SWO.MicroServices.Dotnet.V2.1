using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using Microservices.Security.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity;

namespace Microservices.Security.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
{
    public DbSet<ApplicationClient> ApplicationClient { get; set; }
    public DbSet<Menu> Menus { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("Tbl_Users");
        builder.Entity<ApplicationRole>().ToTable("Tbl_Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("Tbl_UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("Tbl_UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("Tbl_UserLogins");
        builder.Entity<IdentityUserToken<string>>().ToTable("Tbl_UserTokens");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("Tbl_RoleClaims");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new ApplicationClientConfiguration());
        builder.ApplyConfiguration(new MenuConfiguration());
    }
}
