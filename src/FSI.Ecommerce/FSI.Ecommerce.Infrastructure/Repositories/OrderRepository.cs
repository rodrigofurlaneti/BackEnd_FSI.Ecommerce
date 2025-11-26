using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(EcommerceDbContext context) : base(context)
        {
        }

        public Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default)
        {
            return DbSet
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, ct);
        }
    }
}