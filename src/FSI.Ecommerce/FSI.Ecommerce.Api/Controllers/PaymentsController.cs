using FSI.Ecommerce.Application.Dtos.Payments;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public sealed class PaymentsController : ControllerBase
    {
        private readonly IPaymentAppService _paymentAppService;

        public PaymentsController(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }

        [HttpGet("order/{orderId:long}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<PaymentTransactionDto>>> GetByOrderId(
            long orderId,
            CancellationToken ct)
        {
            var list = await _paymentAppService.GetByOrderIdAsync(orderId, ct);
            return Ok(list);
        }
    }
}
