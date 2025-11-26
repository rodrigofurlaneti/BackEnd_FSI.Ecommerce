using FSI.Ecommerce.Application.Dtos.Payments;
using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Application.Interfaces.Services.Payments
{
    public sealed class PaymentAppService : IPaymentAppService
    {
        private readonly IPaymentTransactionRepository _paymentRepository;

        public PaymentAppService(IPaymentTransactionRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IReadOnlyList<PaymentTransactionDto>> GetByOrderIdAsync(
            long orderId,
            CancellationToken ct = default)
        {
            var all = await _paymentRepository.GetAllAsync(ct);
            var filtered = all.Where(x => x.OrderId == orderId).ToList();

            return filtered.Select(p => new PaymentTransactionDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Method = p.Method,
                Status = p.Status,
                Amount = p.Amount.Amount,
                Currency = p.Amount.Currency,
                ProviderTransactionId = p.ProviderTransactionId,
                CreatedAt = p.CreatedAt
            }).ToList();
        }
    }
}