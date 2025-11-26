using FSI.Ecommerce.Domain.Entities;
using FSI.ECommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
