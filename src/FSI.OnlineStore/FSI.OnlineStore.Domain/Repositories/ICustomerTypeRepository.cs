using FSI.OnlineStore.Domain.Entities;

namespace FSI.OnlineStore.Domain.Repositories
{
    public interface ICustomerTypeRepository
    {
        Task<IReadOnlyCollection<CustomerType>> ListAsync(CancellationToken ct);
        Task<CustomerType?> GetByNameAsync(string typeName, CancellationToken ct);
    }
}
