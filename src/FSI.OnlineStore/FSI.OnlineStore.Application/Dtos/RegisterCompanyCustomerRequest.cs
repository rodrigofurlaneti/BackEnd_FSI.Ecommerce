using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Application.Dtos
{
    public sealed class RegisterCompanyCustomerRequest
    {
        public string FullName { get; init; } = string.Empty;      
        public string EmailAddress { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string CorporateName { get; init; } = string.Empty;
        public string? TradeName { get; init; }
        public string CnpjNumber { get; init; } = string.Empty;
        public string? StateRegistration { get; init; }
    }
}
