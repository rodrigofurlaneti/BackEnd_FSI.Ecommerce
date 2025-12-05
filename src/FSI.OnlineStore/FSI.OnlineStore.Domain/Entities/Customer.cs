namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class Customer
    {
        public uint CustomerId { get; private set; }
        public uint CustomerTypeId { get; private set; }

        public string FullName { get; private set; } = string.Empty;
        public string EmailAddress { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string? CpfNumber { get; private set; }

        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Customer() { }

        public Customer(
            uint customerTypeId,
            string fullName,
            string emailAddress,
            string passwordHash,
            string? cpfNumber)
        {
            CustomerTypeId = customerTypeId;
            FullName = fullName;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            CpfNumber = cpfNumber;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateBasicInfo(string fullName, string emailAddress)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
