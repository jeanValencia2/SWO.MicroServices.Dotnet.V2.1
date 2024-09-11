using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWO.Microservices.Dotnet.Shared.Setup.Databases;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microservices.Products.Infrastructure.Data;
using Microservices.Products.Application.Common.Interfaces;

namespace Microservices.Products.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var serviceToken = serviceProvider.GetService<IServiceToken>();

        services.AddMySql<ApplicationDbContext>(configuration.GetSection("Database:MySQL:DatabaseName").Value ?? "");

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
