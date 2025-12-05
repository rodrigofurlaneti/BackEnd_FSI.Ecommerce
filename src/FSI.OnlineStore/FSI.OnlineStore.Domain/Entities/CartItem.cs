namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class CartItem
    {
        public uint CartItemId { get; private set; }
        public uint CartId { get; private set; }
        public uint ProductId { get; private set; }
        public uint Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private CartItem() { }

        internal CartItem(uint productId, uint quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CreatedAt = DateTime.UtcNow;
        }

        internal void Update(uint quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
