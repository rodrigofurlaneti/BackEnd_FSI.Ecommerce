using FSI.Ecommerce.Domain.Enums;

namespace FSI.Ecommerce.Application.Dtos.Carts
{
    public sealed class CartDto
    {
        public long Id { get; init; }
        public long? AccountId { get; init; }
        public string? GuestToken { get; init; }
        public CartStatus Status { get; init; }
        public IReadOnlyList<CartItemDto> Items { get; init; } = Array.Empty<CartItemDto>();
    }
}