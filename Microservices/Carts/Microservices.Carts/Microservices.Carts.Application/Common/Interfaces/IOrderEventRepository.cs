using Microservices.Carts.Application.Common.Interfaces.Generics;
using Microservices.Carts.Application.Dtos;

namespace Microservices.Carts.Application.Common.Interfaces;

public interface IOrderEventRepository
{
    Task<OrderDto> GetAll(CancellationToken cancellationToken = default(CancellationToken));
    Task<OrderDto?> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task Save(OrderDto order, CancellationToken cancellationToken = default(CancellationToken));
}
