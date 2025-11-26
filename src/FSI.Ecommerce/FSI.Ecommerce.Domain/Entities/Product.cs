using FSI.Ecommerce.Domain.Aggregates;
using FSI.Ecommerce.Domain.ValueObjects;

namespace FSI.Ecommerce.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public long? CategoryId { get; private set; }
        public string Sku { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public Money Price { get; private set; } = null!;
        public int StockQuantity { get; private set; }

        public ProductCategory? Category { get; private set; }
        public ICollection<CartItem> CartItems { get; private set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private Product() { }

        public Product(string sku, string name, Money price, long? categoryId = null)
            : base()
        {
            Sku = sku;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            StockQuantity = 0;
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice;
            Touch();
        }

        public void IncreaseStock(int quantity)
        {
            StockQuantity += quantity;
            Touch();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Insufficient stock.");

            StockQuantity -= quantity;
            Touch();
        }
    }
}