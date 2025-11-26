using FSI.Ecommerce.Domain.Entities;
using FSI.ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
    }
}
