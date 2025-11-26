using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(EcommerceDbContext context) : base(context)
        {
        }

        public Task<Cart?> GetOpenCartByAccountIdAsync(long accountId, CancellationToken ct = default)
        {
            return DbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.AccountId == accountId && c.Status == Domain.Enums.CartStatus.Open, ct);
        }

        public Task<Cart?> GetOpenCartByGuestTokenAsync(string guestToken, CancellationToken ct = default)
        {
            return DbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.GuestToken == guestToken && c.Status == Domain.Enums.CartStatus.Open, ct);
        }
    }
}