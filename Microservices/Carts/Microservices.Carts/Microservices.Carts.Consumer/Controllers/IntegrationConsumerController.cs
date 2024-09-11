using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Host;
using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Manager;
using SWO.Microservices.Dotnet.Shared.Comunication.Messages;

namespace Microservices.Carts.Consumer.Controllers;

public class IntegrationConsumerController : ConsumerController<IntegrationMessage>
{
    public IntegrationConsumerController(IConsumerManager<IntegrationMessage> consumerManager) : base(consumerManager)
    {
    }
}
