using SWO.Microservices.Dotnet.Shared.Databases.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Setup.Databases;

public static class MongoDb
{
    public static IServiceCollection AddMongoDbConnectionProvider(this IServiceCollection serviceCollection, IConfiguration configuration, string name = "mongodb")
    {
        return serviceCollection
            .AddMongoDbConnectionProvider()
            .AddMongoDbDatabaseConfiguration(configuration)
            .AddMongoHealthCheck(name);
    }
}