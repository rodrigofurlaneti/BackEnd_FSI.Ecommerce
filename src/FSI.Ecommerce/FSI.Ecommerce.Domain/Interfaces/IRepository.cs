using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
        Task<T?> GetByIdAsync(long id, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
