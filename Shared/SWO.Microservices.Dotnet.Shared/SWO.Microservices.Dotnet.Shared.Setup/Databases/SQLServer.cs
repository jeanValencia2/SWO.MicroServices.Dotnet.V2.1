using SWO.Microservices.Dotnet.Shared.Databases.SqlServer;
using SWO.Microservices.Dotnet.Shared.Discovery;
using SWO.Microservices.Dotnet.Shared.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Setup.Databases;

public static class SQLServer
{
    public static IServiceCollection AddSQLServer<T>(this IServiceCollection serviceCollection, string databaseName) where T : DbContext
    {
        return serviceCollection
            .AddSQLServerDbContext<T>(serviceProvider => GetConnectionString(serviceProvider, databaseName))            
            .AddSQLServerHealthCheck(serviceProvider => GetConnectionString(serviceProvider, databaseName));
    }

    private static async Task<string> GetConnectionString(IServiceProvider serviceProvider, string databaseName)
    {
        ISecretManager secretManager = serviceProvider.GetRequiredService<ISecretManager>();
        IServiceDiscovery serviceDiscovery = serviceProvider.GetRequiredService<IServiceDiscovery>();

        DiscoveryData sqlServerData = await serviceDiscovery.GetDiscoveryData(DiscoveryServices.SQLServer);        
        SQLServerCredentials credentials = await secretManager.Get<SQLServerCredentials>("sqlserver");

        return $"Data Source={sqlServerData.Server},{sqlServerData.Port};Initial Catalog={databaseName};Persist Security Info=False;User ID={credentials.username};Password={credentials.password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }

    private record SQLServerCredentials
    {
        public string username { get; init; } = null!;
        public string password { get; init; } = null!;
    }
}
