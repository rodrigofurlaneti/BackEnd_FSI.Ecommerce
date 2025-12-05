namespace FSI.OnlineStore.Application.Dtos.Product
{
    public sealed class ProductResponse
    {
        public uint ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string SkuCode { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
        public bool IsActive { get; init; }
    }
}
