using SWO.Microservices.Dotnet.Shared.Discovery.Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Setup.Services;

public static class ServiceDiscovery
{
    public static void AddServiceDiscovery(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDiscovery(configuration);
    }
}
