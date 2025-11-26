using FSI.Ecommerce.Domain.Aggregates;

namespace FSI.Ecommerce.Domain.Entities
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public string Code { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public ICollection<AccountUser> AccountUsers { get; private set; } = new List<AccountUser>();

        private Role() { }

        public Role(string code, string name, string? description = null)
            : base()
        {
            Code = code.ToUpperInvariant();
            Name = name;
            Description = description;
        }
    }
}
