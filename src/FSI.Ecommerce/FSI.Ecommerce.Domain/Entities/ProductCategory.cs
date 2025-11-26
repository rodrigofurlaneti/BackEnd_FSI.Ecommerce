namespace FSI.Ecommerce.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public long? ParentId { get; private set; }

        public ProductCategory? ParentCategory { get; private set; }
        public ICollection<ProductCategory> Children { get; private set; } = new List<ProductCategory>();
        public ICollection<Product> Products { get; private set; } = new List<Product>();
        private ProductCategory() { }

        public ProductCategory(string name, string slug, long? parentId = null)
        {
            Name = name;
            Slug = slug;
            ParentId = parentId;
        }

        public void Update(string name, string slug, long? parentId)
        {
            Name = name;
            Slug = slug;
            ParentId = parentId;
            Touch();
        }
    }
}
