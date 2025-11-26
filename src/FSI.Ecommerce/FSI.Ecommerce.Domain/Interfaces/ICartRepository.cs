using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetOpenCartByAccountIdAsync(long accountId, CancellationToken ct = default);
        Task<Cart?> GetOpenCartByGuestTokenAsync(string guestToken, CancellationToken ct = default);
    }
}
