using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.DomainEvents
{
    public sealed class OrderPlacedDomainEvent : DomainEventBase
    {
        public Order Order { get; }

        public OrderPlacedDomainEvent(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }
    }
}