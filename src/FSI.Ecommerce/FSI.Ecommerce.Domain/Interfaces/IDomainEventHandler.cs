namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IDomainEventHandler<in TEvent> where TEvent : FSI.Ecommerce.Domain.DomainEvents.IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent, CancellationToken ct = default);
    }
}
