using Microservices.Carts.Domain.Entities;
using Microservices.Carts.Domain.Enums;
using Microservices.Carts.Domain.ValueObjects;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Carts.Domain.Aggregates
{
    public class Order : BaseAuditableEntity<Guid>
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }


        public DeliveryDetails? deliveryDetails { get; set; }
        public PaymentInformation? paymentInformation { get; set; }

        public List<ProductQuantity>? Products { get; private set; } = new List<ProductQuantity>();


        public void AddProduct(ProductQuantity productQuantity)
        {
            Products.Add(productQuantity);
        }

        public void RemoveProduct(ProductQuantity productQuantity)
        {
            Products.Remove(productQuantity);
        }
    }
}
