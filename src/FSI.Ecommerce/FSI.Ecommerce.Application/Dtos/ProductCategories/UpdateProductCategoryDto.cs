namespace FSI.Ecommerce.Application.Dtos.ProductCategories
{
    public sealed class UpdateProductCategoryDto
    {
        public string Name { get; init; } = null!;
        public string Slug { get; init; } = null!;
        public long? ParentId { get; init; }
    }
}
