namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class CompanyCustomer
    {
        public uint CompanyCustomerId { get; private set; }
        public uint CustomerId { get; private set; }

        public string CorporateName { get; private set; } = string.Empty;
        public string? TradeName { get; private set; }
        public string CnpjNumber { get; private set; } = string.Empty;
        public string? StateRegistration { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private CompanyCustomer() { }

        public CompanyCustomer(
            uint customerId,
            string corporateName,
            string cnpjNumber,
            string? tradeName,
            string? stateRegistration)
        {
            CustomerId = customerId;
            CorporateName = corporateName;
            CnpjNumber = cnpjNumber;
            TradeName = tradeName;
            StateRegistration = stateRegistration;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string corporateName, string? tradeName, string? stateRegistration)
        {
            CorporateName = corporateName;
            TradeName = tradeName;
            StateRegistration = stateRegistration;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
