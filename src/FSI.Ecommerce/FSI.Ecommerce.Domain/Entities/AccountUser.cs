namespace FSI.Ecommerce.Domain.Entities
{
    public class AccountUser : BaseEntity
    {
        public long AccountId { get; private set; }
        public long UserId { get; private set; }
        public long RoleId { get; private set; }
        public bool IsDefaultAccount { get; private set; }

        public Account Account { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public Role Role { get; private set; } = null!;

        private AccountUser() { }

        public AccountUser(long accountId, long userId, long roleId, bool isDefaultAccount)
            : base()
        {
            AccountId = accountId;
            UserId = userId;
            RoleId = roleId;
            IsDefaultAccount = isDefaultAccount;
        }

        public void SetAsDefault()
        {
            IsDefaultAccount = true;
            Touch();
        }
    }
}
