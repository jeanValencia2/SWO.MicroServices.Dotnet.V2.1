using MediatR;
using Microservices.Carts.Application.Products.Commands;
using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Handler;
using SWO.Microservices.Dotnet.Shared.Comunication.Messages;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Carts.Consumer.Handlers;

public class ProductModifiedHandler : IIntegrationMessageHandler<Product>
{
    private IMediator _mediator;

    public ProductModifiedHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(IntegrationMessage<Product> message, CancellationToken cancelToken = default)
    {
        var updateProducts = new UpdateProducts
        {
            Id = message.Content.Id,
            Name = message.Content.Name,
            Description = message.Content.Description,
        };
        await _mediator.Send(updateProducts, cancelToken);
    }
}