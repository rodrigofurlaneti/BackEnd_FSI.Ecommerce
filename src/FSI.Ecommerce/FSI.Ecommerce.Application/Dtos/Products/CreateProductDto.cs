namespace FSI.Ecommerce.Application.Dtos.Products
{
    public sealed class CreateProductDto
    {
        public long? CategoryId { get; init; }
        public string Sku { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = "USD";
        public int InitialStockQuantity { get; init; }
    }
}