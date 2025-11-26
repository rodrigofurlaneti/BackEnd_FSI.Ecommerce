using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
