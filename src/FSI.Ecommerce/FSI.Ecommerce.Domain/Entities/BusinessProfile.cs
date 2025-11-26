using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Entities
{
    public class BusinessProfile
    {
        public long AccountId { get; private set; }
        public string CompanyName { get; private set; } = null!;
        public string? TradeName { get; private set; }
        public string TaxId { get; private set; } = null!;
        public string? StateRegistration { get; private set; }

        public Account Account { get; private set; } = null!;

        private BusinessProfile() { }

        public BusinessProfile(long accountId, string companyName, string? tradeName, string taxId, string? stateRegistration)
        {
            AccountId = accountId;
            CompanyName = companyName;
            TradeName = tradeName;
            TaxId = taxId;
            StateRegistration = stateRegistration;
        }
    }
}
