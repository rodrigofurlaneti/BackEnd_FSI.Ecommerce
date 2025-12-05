using FSI.OnlineStore.Domain.Entities;

namespace FSI.OnlineStore.Domain.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetByIdAsync(uint cartId, CancellationToken ct);
        Task<ShoppingCart?> GetByCustomerAsync(uint customerId, CancellationToken ct);
        Task<ShoppingCart?> GetByVisitorTokenAsync(string visitorToken, CancellationToken ct);
        Task<uint> InsertAsync(ShoppingCart cart, CancellationToken ct);
        Task UpdateAsync(ShoppingCart cart, CancellationToken ct);
        Task DeleteAsync(uint cartId, CancellationToken ct);
    }
}
