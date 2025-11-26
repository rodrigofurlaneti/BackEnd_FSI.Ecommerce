using FSI.Ecommerce.Domain.Enums;
using FSI.Ecommerce.Domain.ValueObjects;

namespace FSI.Ecommerce.Domain.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        public long OrderId { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public Money Amount { get; private set; } = null!;
        public string? ProviderTransactionId { get; private set; }

        public Order Order { get; private set; } = null!;

        private PaymentTransaction() { }

        public PaymentTransaction(long orderId, PaymentMethod method, Money amount)
            : base()
        {
            OrderId = orderId;
            Method = method;
            Amount = amount;
            Status = PaymentStatus.Pending;
        }

        public void MarkCaptured(string providerTransactionId)
        {
            ProviderTransactionId = providerTransactionId;
            Status = PaymentStatus.Captured;
            Touch();
        }

        public void MarkFailed()
        {
            Status = PaymentStatus.Failed;
            Touch();
        }
    }
}