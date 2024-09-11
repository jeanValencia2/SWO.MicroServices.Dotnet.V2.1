using Microservices.Carts.Application.Common.Interfaces.Generics;
using Microservices.Carts.Domain.Aggregates;

namespace Microservices.Carts.Application.Common.Interfaces;

public interface IOrderRepository: IGenericRepository<Order>
{
}
