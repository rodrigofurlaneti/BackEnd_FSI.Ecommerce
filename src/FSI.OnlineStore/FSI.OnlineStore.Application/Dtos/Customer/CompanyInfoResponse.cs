namespace FSI.OnlineStore.Application.Dtos.Customer
{
    public sealed class CompanyInfoResponse
    {
        public string CorporateName { get; init; } = string.Empty;
        public string? TradeName { get; init; }
        public string CnpjNumber { get; init; } = string.Empty;
        public string? StateRegistration { get; init; }
    }
}
