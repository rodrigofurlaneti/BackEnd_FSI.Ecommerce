using Microsoft.AspNetCore.Mvc;
using FSI.OnlineStore.Application.UseCases;
using FSI.OnlineStore.Application.Dtos;

namespace FSI.OnlineStore.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public sealed class CartController : ControllerBase
    {
        private readonly AddCartItemUseCase _addCartItemUseCase;

        public CartController(AddCartItemUseCase addCartItemUseCase)
        {
            _addCartItemUseCase = addCartItemUseCase;
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request, CancellationToken ct)
        {
            var cart = await _addCartItemUseCase.ExecuteAsync(request, ct);
            return Ok(new
            {
                cart.CartId,
                cart.CustomerId,
                cart.VisitorToken,
                cart.CartStatus,
                Items = cart.Items.Select(i => new
                {
                    i.CartItemId,
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                }),
                Total = cart.GetTotal()
            });
        }
    }
}
