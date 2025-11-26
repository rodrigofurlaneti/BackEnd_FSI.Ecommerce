using FSI.Ecommerce.Domain.Entities;

namespace FSI.ECommerce.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Returns all active entities.
        /// Infrastructure layer should normally apply a global filter on IsActive = true.
        /// </summary>
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);

        /// <summary>
        /// Returns an entity by its identifier, or null if not found.
        /// </summary>
        Task<T?> GetByIdAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Adds a new entity to the underlying store.
        /// </summary>
        Task AddAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        Task UpdateAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Deletes an entity.
        /// In most cases this should be implemented as a soft delete
        /// (setting IsActive = false and DeletedAt = now) instead of a hard delete.
        /// </summary>
        Task DeleteAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Returns a paged list of active entities.
        /// pageNumber starts at 1.
        /// </summary>
        Task<IReadOnlyList<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);
    }
}
