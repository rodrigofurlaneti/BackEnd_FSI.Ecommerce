using FSI.Ecommerce.Domain.Aggregates;

namespace FSI.Ecommerce.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        public ICollection<AccountUser> AccountUsers { get; private set; } = new List<AccountUser>();

        private User() { }

        public User(string email, string passwordHash)
            : base()
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public void SetPasswordHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Password hash must be provided.", nameof(hash));

            PasswordHash = hash;
            Touch();
        }
    }
}
