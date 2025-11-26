namespace FSI.Ecommerce.Domain.DomainEvents
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public DateTime OccurredAt { get; }

        protected DomainEventBase()
        {
            OccurredAt = DateTime.UtcNow;
        }
    }
}