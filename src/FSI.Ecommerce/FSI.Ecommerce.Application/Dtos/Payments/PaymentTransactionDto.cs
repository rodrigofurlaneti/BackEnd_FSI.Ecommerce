using FSI.Ecommerce.Domain.Enums;

namespace FSI.Ecommerce.Application.Dtos.Payments
{
    public sealed class PaymentTransactionDto
    {
        public long Id { get; init; }
        public long OrderId { get; init; }
        public PaymentMethod Method { get; init; }
        public PaymentStatus Status { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = null!;
        public string? ProviderTransactionId { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}