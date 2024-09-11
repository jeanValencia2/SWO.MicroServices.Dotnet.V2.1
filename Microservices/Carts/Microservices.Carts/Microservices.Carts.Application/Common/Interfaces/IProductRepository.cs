using Microservices.Carts.Application.Common.Interfaces.Generics;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Carts.Application.Common.Interfaces;

public interface IProductRepository : IUpdateRepository<Product>
{   
}
