using FSI.Ecommerce.Domain.Enums;

namespace FSI.Ecommerce.Application.Dtos.Orders
{
    public sealed class OrderSummaryDto
    {
        public long Id { get; init; }
        public string OrderNumber { get; init; } = null!;
        public OrderStatus Status { get; init; }
        public decimal TotalAmount { get; init; }
        public string Currency { get; init; } = null!;
        public DateTime CreatedAt { get; init; }
    }
}