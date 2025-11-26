namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent, CancellationToken ct = default);
    }
}
