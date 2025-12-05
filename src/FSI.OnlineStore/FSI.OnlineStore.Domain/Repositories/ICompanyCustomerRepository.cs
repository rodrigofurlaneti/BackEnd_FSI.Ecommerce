using FSI.OnlineStore.Domain.Entities;

namespace FSI.OnlineStore.Domain.Repositories
{
    public interface ICompanyCustomerRepository
    {
        Task<uint> InsertAsync(CompanyCustomer company, CancellationToken ct);
        Task UpdateAsync(CompanyCustomer company, CancellationToken ct);
        Task<CompanyCustomer?> GetByCustomerIdAsync(uint customerId, CancellationToken ct);
    }
}
