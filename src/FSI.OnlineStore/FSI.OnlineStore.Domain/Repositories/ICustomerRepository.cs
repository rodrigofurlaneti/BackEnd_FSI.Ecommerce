using FSI.OnlineStore.Domain.Entities;

namespace FSI.OnlineStore.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<uint> InsertAsync(Customer customer, CancellationToken ct);
        Task UpdateAsync(Customer customer, CancellationToken ct);
        Task<Customer?> GetByIdAsync(uint customerId, CancellationToken ct);
        Task<Customer?> GetByEmailAsync(string emailAddress, CancellationToken ct);
        Task<IReadOnlyCollection<Customer>> ListAsync(CancellationToken ct);
        Task DeleteAsync(uint customerId, CancellationToken ct);
    }
}
