using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface ICartDomainService
    {
        Cart AddOrUpdateItem(Cart cart, long productId, int quantity, decimal unitPrice);
    }
}
