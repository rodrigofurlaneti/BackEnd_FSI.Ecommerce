namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class ShoppingCart
    {
        public uint CartId { get; private set; }
        public uint? CustomerId { get; private set; }
        public string? VisitorToken { get; private set; }
        public string CartStatus { get; private set; } = "Active";
        public DateTime CreatedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        private ShoppingCart() { }

        public ShoppingCart(uint? customerId, string? visitorToken, DateTime? expiresAt)
        {
            CustomerId = customerId;
            VisitorToken = visitorToken;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
        }

        public CartItem AddOrUpdateItem(uint productId, uint quantity, decimal unitPrice)
        {
            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing is not null)
            {
                existing.Update(quantity, unitPrice);
                return existing;
            }

            var item = new CartItem(productId, quantity, unitPrice);
            _items.Add(item);
            return item;
        }

        public decimal GetTotal() => _items.Sum(i => i.Quantity * i.UnitPrice);
    }
}
