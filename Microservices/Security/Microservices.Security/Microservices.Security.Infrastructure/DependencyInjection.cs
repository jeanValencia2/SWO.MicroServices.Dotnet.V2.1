using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using Microservices.Security.Infrastructure.Data;
using Microservices.Security.Infrastructure.Identity;
using SWO.Microservices.Dotnet.Shared.Setup.Databases;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Security.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();        
        var serviceToken = serviceProvider.GetService<IServiceToken>();        

        services.AddSQLServer<ApplicationDbContext>(configuration.GetSection("Database:SQLServer:DatabaseName").Value ?? "");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = serviceToken!.GetTokenValidationParameters().Result;
        });
        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()            
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}
