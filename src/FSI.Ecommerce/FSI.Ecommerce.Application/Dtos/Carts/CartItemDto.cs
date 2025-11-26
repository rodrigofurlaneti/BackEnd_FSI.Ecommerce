namespace FSI.Ecommerce.Application.Dtos.Carts
{
    public sealed class CartItemDto
    {
        public long ProductId { get; init; }
        public string ProductName { get; init; } = null!;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}