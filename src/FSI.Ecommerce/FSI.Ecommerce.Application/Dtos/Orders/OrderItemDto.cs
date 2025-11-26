namespace FSI.Ecommerce.Application.Dtos.Orders
{
    public sealed class OrderItemDto
    {
        public long ProductId { get; init; }
        public string ProductName { get; init; } = null!;
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public decimal LineTotal { get; init; }
    }
}