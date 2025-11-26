using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _dbContext;

        public ProductRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Product>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Sku == sku, ct);
        }

        public async Task AddAsync(Product entity, CancellationToken ct = default)
        {
            await _dbContext.Products.AddAsync(entity, ct);
        }

        public Task UpdateAsync(Product entity, CancellationToken ct = default)
        {
            _dbContext.Products.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Product entity, CancellationToken ct = default)
        {
            _dbContext.Products.Remove(entity);
            return Task.CompletedTask;
        }
    }
}