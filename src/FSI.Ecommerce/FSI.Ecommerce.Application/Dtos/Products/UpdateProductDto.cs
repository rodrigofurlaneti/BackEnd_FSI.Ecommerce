namespace FSI.Ecommerce.Application.Dtos.Products
{
    public sealed class UpdateProductDto
    {
        public long? CategoryId { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = "USD";
        public bool IsActive { get; init; }
    }
}