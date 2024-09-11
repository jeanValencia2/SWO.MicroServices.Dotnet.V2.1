using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWO.Microservices.Dotnet.Shared.EventSourcing;

namespace SWO.Microservices.Dotnet.Shared.Setup.Services;

public static class EventSourcing
{
    public static void AddEventSourcing(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddMongoEventSourcing(configuration);
    }
}
