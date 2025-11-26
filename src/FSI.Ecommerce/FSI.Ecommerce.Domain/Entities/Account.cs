using FSI.Ecommerce.Domain.Aggregates;
using FSI.Ecommerce.Domain.Enums;

namespace FSI.Ecommerce.Domain.Entities
{
    public class Account : BaseEntity, IAggregateRoot
    {
        public AccountType AccountType { get; private set; }
        public string DisplayName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? PhoneNumber { get; private set; }

        public IndividualProfile? IndividualProfile { get; private set; }
        public BusinessProfile? BusinessProfile { get; private set; }
        public ICollection<AccountUser> AccountUsers { get; private set; } = new List<AccountUser>();
        public ICollection<AccountAddress> Addresses { get; private set; } = new List<AccountAddress>();
        public ICollection<Cart> Carts { get; private set; } = new List<Cart>();
        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        private Account() { }

        public Account(AccountType accountType, string displayName, string email, string? phoneNumber)
            : base()
        {
            AccountType = accountType;
            DisplayName = displayName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void UpdateContact(string email, string? phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Touch();
        }
    }
}
