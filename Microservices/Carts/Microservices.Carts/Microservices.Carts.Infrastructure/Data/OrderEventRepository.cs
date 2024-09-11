using Microservices.Carts.Application.Common.Interfaces;
using Microservices.Carts.Application.Dtos;
using SWO.Microservices.Dotnet.Shared.EventSourcing;
using SWO.Microservices.Dotnet.Shared.EventSourcing.EventStores;
using System.Threading;
using VaultSharp.V1.SecretsEngines.Identity;

namespace Microservices.Carts.Infrastructure.Data;

public class OrderEventRepository : AggregateRepository<OrderDto>, IOrderEventRepository
{
    public OrderEventRepository(IEventStore eventStore) : base(eventStore)
    {
    }

    public async Task<OrderDto> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task<OrderDto> GetAll(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task Save(OrderDto order, CancellationToken cancellationToken = default)
    {
        await SaveAsync(order, cancellationToken);
    }
}
