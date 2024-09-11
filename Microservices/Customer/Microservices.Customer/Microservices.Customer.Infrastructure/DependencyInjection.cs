using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.Setup.Databases;
using Microservices.Customer.Infrastructure.Data;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Microservices.Customer.Infrastructure;

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

        return services;
    }
}
