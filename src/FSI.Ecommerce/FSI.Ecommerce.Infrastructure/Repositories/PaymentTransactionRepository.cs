using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class PaymentTransactionRepository
        : RepositoryBase<PaymentTransaction>, IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(EcommerceDbContext context) : base(context)
        {
        }
    }
}