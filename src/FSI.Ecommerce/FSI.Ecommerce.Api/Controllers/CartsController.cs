using FSI.Ecommerce.Application.Dtos.Carts;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public sealed class CartsController : ControllerBase
    {
        private readonly ICartAppService _cartAppService;

        public CartsController(ICartAppService cartAppService)
        {
            _cartAppService = cartAppService;
        }

        [HttpGet("account/{accountId:long}")]
        [Authorize]
        public async Task<ActionResult<CartDto>> GetCartForAccount(long accountId, CancellationToken ct)
        {
            var cart = await _cartAppService.GetOrCreateCartForAccountAsync(accountId, ct);
            return Ok(cart);
        }

        [HttpPost("account/{accountId:long}/items")]
        [Authorize]
        public async Task<ActionResult<CartDto>> AddItemForAccount(
            long accountId,
            [FromBody] AddCartItemRequestDto dto,
            CancellationToken ct)
        {
            var cart = await _cartAppService.AddItemForAccountAsync(accountId, dto, ct);
            return Ok(cart);
        }

        [HttpGet("guest/{guestToken}")]
        [AllowAnonymous]
        public async Task<ActionResult<CartDto>> GetCartForGuest(string guestToken, CancellationToken ct)
        {
            var cart = await _cartAppService.GetOrCreateCartForGuestAsync(guestToken, ct);
            return Ok(cart);
        }

        [HttpPost("guest/{guestToken}/items")]
        [AllowAnonymous]
        public async Task<ActionResult<CartDto>> AddItemForGuest(
            string guestToken,
            [FromBody] AddCartItemRequestDto dto,
            CancellationToken ct)
        {
            var cart = await _cartAppService.AddItemForGuestAsync(guestToken, dto, ct);
            return Ok(cart);
        }
    }
}
