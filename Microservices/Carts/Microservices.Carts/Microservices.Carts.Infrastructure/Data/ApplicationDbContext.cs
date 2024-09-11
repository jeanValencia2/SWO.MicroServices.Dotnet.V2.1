
using Microservices.Carts.Application.Common.Interfaces;

namespace Microservices.Carts.Infrastructure.Data;

public class ApplicationDbContext : IApplicationDbContext
{
    public IOrderRepository OrderRepository { get ; set ; }
    public IProductRepository ProductRepository { get; set; }
    public IOrderEventRepository OrderEventRepository { get; set; }

    public ApplicationDbContext(IOrderRepository orderRepository, IProductRepository productRepository, IOrderEventRepository orderEventRepository)
    {
        OrderRepository = orderRepository;
        ProductRepository = productRepository;
        OrderEventRepository = orderEventRepository;
    }
}
