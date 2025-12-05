using FSI.OnlineStore.Domain.Entities;

namespace FSI.OnlineStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<uint> InsertAsync(Product product, CancellationToken ct);
        Task UpdateAsync(Product product, CancellationToken ct);
        Task<Product?> GetByIdAsync(uint productId, CancellationToken ct);
        Task<IReadOnlyCollection<Product>> ListAsync(CancellationToken ct);
        Task DeleteAsync(uint productId, CancellationToken ct);
        Task<IReadOnlyCollection<ProductPrice>> ListPricesByProductAsync(uint productId, CancellationToken ct);
    }
}
