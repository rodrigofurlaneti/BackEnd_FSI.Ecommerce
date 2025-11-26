namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IDomainEvent
    {
        DateTime OccurredAt { get; }
    }
}
