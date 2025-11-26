namespace FSI.Ecommerce.Application.Dtos.Products
{
    public sealed class ProductDto
    {
        public long Id { get; init; }
        public long? CategoryId { get; init; }
        public string Sku { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = null!;
        public int StockQuantity { get; init; }
        public bool IsActive { get; init; }
    }
}