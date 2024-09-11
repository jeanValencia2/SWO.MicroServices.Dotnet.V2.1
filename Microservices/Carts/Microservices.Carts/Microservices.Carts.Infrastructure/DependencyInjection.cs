using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microservices.Carts.Infrastructure.Data;
using SWO.Microservices.Dotnet.Shared.Setup.Databases;
using Microservices.Carts.Application.Common.Interfaces;

namespace Microservices.Carts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbConnectionProvider(configuration);

        services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderEventRepository, OrderEventRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
