namespace FSI.Ecommerce.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }
        public bool IsActive { get; protected set; } = true;
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        private readonly List<FSI.Ecommerce.Domain.DomainEvents.IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<FSI.Ecommerce.Domain.DomainEvents.IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            Touch();
        }

        public void Deactivate()
        {
            IsActive = false;
            Touch();
        }

        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddDomainEvent(FSI.Ecommerce.Domain.DomainEvents.IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
