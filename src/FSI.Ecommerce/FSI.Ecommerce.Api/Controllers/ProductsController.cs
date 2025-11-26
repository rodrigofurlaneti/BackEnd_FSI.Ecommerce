using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.Products;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResultDto<ProductDto>>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _productAppService.GetPagedAsync(pageNumber, pageSize, ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDto>> GetById(long id, CancellationToken ct)
        {
            var product = await _productAppService.GetByIdAsync(id, ct);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDto>> Create(
            [FromBody] CreateProductDto dto,
            CancellationToken ct)
        {
            var created = await _productAppService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public async Task<ActionResult<ProductDto>> Update(
            long id,
            [FromBody] UpdateProductDto dto,
            CancellationToken ct)
        {
            var updated = await _productAppService.UpdateAsync(id, dto, ct);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _productAppService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
