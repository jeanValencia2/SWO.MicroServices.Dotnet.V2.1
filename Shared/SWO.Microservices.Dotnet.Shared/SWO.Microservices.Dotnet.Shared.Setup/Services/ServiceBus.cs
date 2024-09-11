using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Handler;
using SWO.Microservices.Dotnet.Shared.Comunication.Messages;
using SWO.Microservices.Dotnet.Shared.Comunication.RabbitMQ;
using SWO.Microservices.Dotnet.Shared.Discovery;
using SWO.Microservices.Dotnet.Shared.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amazon.SecurityToken.Model;

namespace SWO.Microservices.Dotnet.Shared.Setup.Services;

public static class ServiceBus
{
    public static void AddServiceBusIntegrationPublisher(this IServiceCollection serviceCollection,
       IConfiguration configuration)
    {
        serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName,
            configuration, "IntegrationPublisher");
        serviceCollection.AddRabbitMQPublisher<IntegrationMessage>();
    }

    /// <summary>
    /// default option (KeyValue) to get credentials using Vault 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    private static async Task<RabbitMQCredentials> GetRabbitMqSecretCredentials(IServiceProvider serviceProvider)
    {
        var secretManager = serviceProvider.GetService<ISecretManager>();
        RabbitMQCredentials credentials = await secretManager!.Get<RabbitMQCredentials>("rabbitmq");
        return credentials;
    }   

    public static void AddServiceBusIntegrationConsumer(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration,
            "IntegrationConsumer");
        serviceCollection.AddRabbitMqConsumer<IntegrationMessage>();
    }

    public static void AddServiceBusDomainPublisher(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration,
            "DomainPublisher");
        serviceCollection.AddRabbitMQPublisher<DomainMessage>();
    }

    public static void AddServiceBusDomainConsumer(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration,
            "DomainConsumer");
        serviceCollection.AddRabbitMqConsumer<DomainMessage>();
    }

    public static void AddHandlersInAssembly<T>(this IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(scan => scan.FromAssemblyOf<T>()
            .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        ServiceProvider sp = serviceCollection.BuildServiceProvider();
        var listHandlers = sp.GetServices<IMessageHandler>();
        serviceCollection.AddConsumerHandlers(listHandlers);
    }

    private static async Task<string> GetRabbitMQHostName(IServiceProvider serviceProvider)
    {
        var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
        return await serviceDiscovery?.GetFullAddress(DiscoveryServices.RabbitMQ)!;
    }
}
