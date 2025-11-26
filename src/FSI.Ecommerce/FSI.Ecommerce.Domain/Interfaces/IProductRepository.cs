using FSI.Ecommerce.Domain.Entities;
using FSI.ECommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default);
    }
}
