using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Domain.Services
{
    public sealed class CartDomainService : ICartDomainService
    {
        public Cart AddOrUpdateItem(Cart cart, long productId, int quantity, decimal unitPrice)
        {
            if (cart is null) throw new ArgumentNullException(nameof(cart));
            cart.AddItem(productId, quantity, unitPrice);
            return cart;
        }
    }
}