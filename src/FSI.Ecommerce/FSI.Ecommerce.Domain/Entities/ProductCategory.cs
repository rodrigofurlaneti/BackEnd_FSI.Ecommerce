using FSI.Ecommerce.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Entities
{
    public class ProductCategory : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public long? ParentId { get; private set; }

        public ProductCategory? Parent { get; private set; }
        public ICollection<ProductCategory> Children { get; private set; } = new List<ProductCategory>();
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        private ProductCategory() { }

        public ProductCategory(string name, string slug, long? parentId = null)
            : base()
        {
            Name = name;
            Slug = slug;
            ParentId = parentId;
        }
    }
}