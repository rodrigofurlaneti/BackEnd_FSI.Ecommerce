using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly EcommerceDbContext Context;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(EcommerceDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await DbSet.AsNoTracking().ToListAsync(ct);
        }

        public virtual async Task<T?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await DbSet.FindAsync(new object[] { id }, ct);
        }

        public virtual async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await DbSet.AddAsync(entity, ct);
        }

        public virtual Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            return await DbSet
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }
    }
}
