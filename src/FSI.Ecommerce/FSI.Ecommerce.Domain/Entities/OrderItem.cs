using FSI.Ecommerce.Domain.Aggregates;
using FSI.Ecommerce.Domain.ValueObjects;

namespace FSI.Ecommerce.Domain.Entities
{
    public class OrderItem : BaseEntity, IAggregateRoot
    {
        public long OrderId { get; private set; }
        public long ProductId { get; private set; }
        public string ProductName { get; private set; } = null!;
        public Money UnitPrice { get; private set; } = null!;
        public int Quantity { get; private set; }
        public Money LineTotal { get; private set; } = null!;
        public Order Order { get; private set; } = null!;
        public Product? Product { get; private set; }
        private OrderItem() { }

        public OrderItem(
            long orderId,
            long productId,
            string productName,
            Money unitPrice,
            int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            LineTotal = unitPrice.Multiply(quantity);
        }
    }
}
