namespace Microservices.Carts.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public IOrderRepository OrderRepository { get; set; }
    public IOrderEventRepository OrderEventRepository { get; set; }
    public IProductRepository ProductRepository { get; set; }
}
