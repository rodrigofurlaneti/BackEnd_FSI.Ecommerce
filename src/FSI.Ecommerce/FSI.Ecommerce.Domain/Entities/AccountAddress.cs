namespace FSI.Ecommerce.Domain.Entities
{
    public class AccountAddress : BaseEntity
    {
        public long AccountId { get; private set; }
        public string Label { get; private set; } = null!;
        public string Line1 { get; private set; } = null!;
        public string? Line2 { get; private set; }
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public string CountryCode { get; private set; } = null!;
        public bool IsDefaultShipping { get; private set; }
        public bool IsDefaultBilling { get; private set; }

        public Account Account { get; private set; } = null!;

        private AccountAddress() { }

        public AccountAddress(
            long accountId,
            string label,
            string line1,
            string? line2,
            string city,
            string state,
            string postalCode,
            string countryCode,
            bool isDefaultShipping,
            bool isDefaultBilling)
            : base()
        {
            AccountId = accountId;
            Label = label;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            PostalCode = postalCode;
            CountryCode = countryCode;
            IsDefaultShipping = isDefaultShipping;
            IsDefaultBilling = isDefaultBilling;
        }

        public void MarkAsDefaultShipping()
        {
            IsDefaultShipping = true;
            Touch();
        }

        public void MarkAsDefaultBilling()
        {
            IsDefaultBilling = true;
            Touch();
        }
    }
}