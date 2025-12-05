namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class Product
    {
        public uint ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public string SkuCode { get; private set; } = string.Empty;
        public decimal BasePrice { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Product() { }

        public Product(string productName, string skuCode, decimal basePrice)
        {
            ProductName = productName;
            SkuCode = skuCode;
            BasePrice = basePrice;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string productName, string skuCode, decimal basePrice, bool isActive)
        {
            ProductName = productName;
            SkuCode = skuCode;
            BasePrice = basePrice;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
