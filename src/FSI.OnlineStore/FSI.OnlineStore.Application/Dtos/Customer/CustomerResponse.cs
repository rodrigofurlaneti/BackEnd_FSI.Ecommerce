namespace FSI.OnlineStore.Application.Dtos.Customer
{
    public sealed class CustomerResponse
    {
        public uint CustomerId { get; init; }
        public uint CustomerTypeId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string EmailAddress { get; init; } = string.Empty;
        public string? CpfNumber { get; init; }
        public bool IsActive { get; init; }
        public CompanyInfoResponse? Company { get; init; }
    }
}
