using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Host;
using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Manager;
using SWO.Microservices.Dotnet.Shared.Comunication.Messages;
using SWO.Microservices.Dotnet.Shared.Comunication.Publisher.Domain;
using SWO.Microservices.Dotnet.Shared.Comunication.Publisher.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SWO.Microservices.Dotnet.Shared.Comunication;

public static class CommunicationDependencyInjection
{
    public static void AddConsumer<TMessage>(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsumerManager<TMessage>, ConsumerManager<TMessage>>();
        serviceCollection.AddSingleton<IHostedService, ConsumerHostedService<TMessage>>();
    }

    public static void AddPublisher<TMessage>(this IServiceCollection serviceCollection)
    {
        if (typeof(TMessage) == typeof(IntegrationMessage))
        {
            serviceCollection.AddIntegrationBusPublisher();
        }
        else if (typeof(TMessage) == typeof(DomainMessage))
        {
            serviceCollection.AddDomainBusPublisher();
        }
    }

    private static void AddIntegrationBusPublisher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IIntegrationMessagePublisher, DefaultIntegrationMessagePublisher>();
    }


    private static void AddDomainBusPublisher(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDomainMessagePublisher, DefaultDomainMessagePublisher>();
    }
}