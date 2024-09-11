using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Databases.SqlServer;

public static class SQLServerDependencyInjection
{
    public static IServiceCollection AddSQLServerDbContext<T>(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
        where T : DbContext
    {
        return serviceCollection.AddDbContext<T>((serviceProvider, builder) =>
        {
            builder.UseSqlServer(connectionString.Invoke(serviceProvider).Result);
        });
    }

    public static IServiceCollection AddSQLServerHealthCheck(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
    {
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        string mySqlConnectionString = connectionString.Invoke(serviceProvider).Result;
        serviceCollection.AddHealthChecks().AddSqlServer(mySqlConnectionString);
        return serviceCollection;
    }
}
