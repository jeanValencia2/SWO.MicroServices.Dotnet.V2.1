using SWO.Microservices.Dotnet.Shared.Databases.MySql;
using SWO.Microservices.Dotnet.Shared.Discovery;
using SWO.Microservices.Dotnet.Shared.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Setup.Databases;

public static class MySql
{
    public static IServiceCollection AddMySql<T>(this IServiceCollection serviceCollection, string databaseName)
        where T : DbContext
    {
        return serviceCollection
            .AddMySqlDbContext<T>(serviceProvider => GetConnectionString(serviceProvider, databaseName))
            .AddMysqlHealthCheck(serviceProvider => GetConnectionString(serviceProvider, databaseName));
    }

    private static async Task<string> GetConnectionString(IServiceProvider serviceProvider, string databaseName)
    {
        ISecretManager secretManager = serviceProvider.GetRequiredService<ISecretManager>();
        IServiceDiscovery serviceDiscovery = serviceProvider.GetRequiredService<IServiceDiscovery>();

        DiscoveryData mysqlData = await serviceDiscovery.GetDiscoveryData(DiscoveryServices.MySql);
        MySqlCredentials credentials = await secretManager.Get<MySqlCredentials>("mysql");

        return
            $"Server={mysqlData.Server};Port={mysqlData.Port};Database={databaseName};Uid={credentials.username};password={credentials.password};";
    }


    private record MySqlCredentials
    {
        public string username { get; init; } = null!;
        public string password { get; init; } = null!;
    }
}