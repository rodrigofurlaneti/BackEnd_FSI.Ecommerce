namespace FSI.Ecommerce.Application.Dtos.Carts
{
    public sealed class AddCartItemRequestDto
    {
        public long ProductId { get; init; }
        public int Quantity { get; init; }
    }
}