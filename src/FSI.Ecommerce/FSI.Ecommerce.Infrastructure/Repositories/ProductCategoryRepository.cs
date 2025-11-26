using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(EcommerceDbContext context) : base(context)
        {
        }
    }
}