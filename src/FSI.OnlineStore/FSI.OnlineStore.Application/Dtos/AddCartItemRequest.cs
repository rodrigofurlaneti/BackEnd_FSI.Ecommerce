namespace FSI.OnlineStore.Application.Dtos
{
    public sealed class AddCartItemRequest
    {
        public uint? CustomerId { get; init; }
        public string? VisitorToken { get; init; }
        public uint ProductId { get; init; }
        public uint Quantity { get; init; }
    }
}
