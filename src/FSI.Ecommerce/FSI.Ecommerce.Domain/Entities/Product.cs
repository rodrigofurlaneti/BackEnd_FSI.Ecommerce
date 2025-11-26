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
        public bool IsActive { get; private set; } = true;

        public ProductCategory? Category { get; private set; }
        public ICollection<CartItem> CartItems { get; private set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private Product() { } // EF Core

        public Product(string sku, string name, Money price, long? categoryId) : base()
        {
            Sku = sku;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }

        public Product(
            long? categoryId,
            string sku,
            string name,
            string? description,
            Money price,
            int initialStockQuantity)
            : base()
        {
            CategoryId = categoryId;
            Sku = sku;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = initialStockQuantity;
            IsActive = true;
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice;
            Touch();
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            StockQuantity += quantity;
            Touch();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            if (quantity > StockQuantity)
                throw new InvalidOperationException("Insufficient stock.");

            StockQuantity -= quantity;
            Touch();
        }

        public void Update(
            string name,
            string? description,
            Money price,
            int stockQuantity,
            bool isActive)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
            Touch();
        }
    }
}
