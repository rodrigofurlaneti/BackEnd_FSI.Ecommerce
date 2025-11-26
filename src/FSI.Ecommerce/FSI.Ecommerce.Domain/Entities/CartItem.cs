namespace FSI.Ecommerce.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public long CartId { get; private set; }
        public long ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Cart Cart { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

        private CartItem() { }

        public CartItem(long cartId, long productId, int quantity, decimal unitPrice)
            : base()
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            Quantity += quantity;
            Touch();
        }
    }
}