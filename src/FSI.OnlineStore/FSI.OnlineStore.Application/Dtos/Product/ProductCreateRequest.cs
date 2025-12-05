namespace FSI.OnlineStore.Application.Dtos.Product
{
    public sealed class ProductCreateRequest
    {
        public string ProductName { get; init; } = string.Empty;
        public string SkuCode { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
    }
}
