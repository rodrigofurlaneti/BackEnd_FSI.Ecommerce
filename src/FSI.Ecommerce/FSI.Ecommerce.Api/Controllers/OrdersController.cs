using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.Orders;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrdersController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResultDto<OrderSummaryDto>>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _orderAppService.GetPagedAsync(pageNumber, pageSize, ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<ActionResult<OrderDetailDto>> GetById(long id, CancellationToken ct)
        {
            var order = await _orderAppService.GetByIdAsync(id, ct);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("number/{orderNumber}")]
        [Authorize]
        public async Task<ActionResult<OrderDetailDto>> GetByOrderNumber(string orderNumber, CancellationToken ct)
        {
            var order = await _orderAppService.GetByOrderNumberAsync(orderNumber, ct);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("account/{accountId:long}/from-cart")]
        [Authorize]
        public async Task<ActionResult<OrderDetailDto>> PlaceOrderFromAccountCart(
            long accountId,
            [FromQuery] long? userId,
            CancellationToken ct)
        {
            var order = await _orderAppService.PlaceOrderFromCartForAccountAsync(accountId, userId, ct);
            return Ok(order);
        }

        [HttpPost("guest/{guestToken}/from-cart")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderDetailDto>> PlaceOrderFromGuestCart(
            string guestToken,
            CancellationToken ct)
        {
            var order = await _orderAppService.PlaceOrderFromCartForGuestAsync(guestToken, ct);
            return Ok(order);
        }
    }
}
