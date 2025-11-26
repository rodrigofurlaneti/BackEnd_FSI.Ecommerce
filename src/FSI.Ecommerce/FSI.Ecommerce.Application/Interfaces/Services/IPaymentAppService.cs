using FSI.Ecommerce.Application.Dtos.Payments;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface IPaymentAppService
    {
        Task<IReadOnlyList<PaymentTransactionDto>> GetByOrderIdAsync(long orderId, CancellationToken ct = default);
    }
}
