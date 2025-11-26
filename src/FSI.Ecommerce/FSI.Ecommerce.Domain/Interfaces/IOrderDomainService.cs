using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IOrderDomainService
    {
        Order CreateOrderFromCart(Cart cart, Account account, long? userId);
    }
}
