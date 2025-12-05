namespace FSI.OnlineStore.Application.Dtos
{
    public sealed class RegisterIndividualCustomerRequest
    {
        public string FullName { get; init; } = string.Empty;
        public string EmailAddress { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string CpfNumber { get; init; } = string.Empty;
    }
}
