using FSI.Ecommerce.Domain.Enums;

namespace FSI.Ecommerce.Application.Dtos.Orders
{
    public sealed class OrderDetailDto
    {
        public long Id { get; init; }
        public string OrderNumber { get; init; } = null!;
        public long AccountId { get; init; }
        public long? PlacedByUserId { get; init; }
        public OrderStatus Status { get; init; }
        public decimal TotalAmount { get; init; }
        public string Currency { get; init; } = null!;
        public DateTime CreatedAt { get; init; }

        public string ShippingName { get; init; } = null!;
        public string ShippingLine1 { get; init; } = null!;
        public string? ShippingLine2 { get; init; }
        public string ShippingCity { get; init; } = null!;
        public string ShippingState { get; init; } = null!;
        public string ShippingPostalCode { get; init; } = null!;
        public string ShippingCountryCode { get; init; } = null!;

        public string BillingName { get; init; } = null!;
        public string BillingLine1 { get; init; } = null!;
        public string? BillingLine2 { get; init; }
        public string BillingCity { get; init; } = null!;
        public string BillingState { get; init; } = null!;
        public string BillingPostalCode { get; init; } = null!;
        public string BillingCountryCode { get; init; } = null!;

        public IReadOnlyList<OrderItemDto> Items { get; init; } = Array.Empty<OrderItemDto>();
    }
}