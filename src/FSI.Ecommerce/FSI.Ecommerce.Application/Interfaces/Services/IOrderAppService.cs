using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.Orders;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface IOrderAppService
    {
        Task<OrderDetailDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<OrderDetailDto?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
        Task<PagedResultDto<OrderSummaryDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
        Task<OrderDetailDto> PlaceOrderFromCartForAccountAsync(long accountId, long? userId, CancellationToken ct = default);
        Task<OrderDetailDto> PlaceOrderFromCartForGuestAsync(string guestToken, CancellationToken ct = default);
    }
}