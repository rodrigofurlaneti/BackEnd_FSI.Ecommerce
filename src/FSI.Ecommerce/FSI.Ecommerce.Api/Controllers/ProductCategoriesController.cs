using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.ProductCategories;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/product-categories")]
    public sealed class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryAppService _categoryAppService;

        public ProductCategoriesController(IProductCategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResultDto<ProductCategoryDto>>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50,
            CancellationToken ct = default)
        {
            var result = await _categoryAppService.GetPagedAsync(pageNumber, pageSize, ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductCategoryDto>> GetById(long id, CancellationToken ct)
        {
            var category = await _categoryAppService.GetByIdAsync(id, ct);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductCategoryDto>> Create(
            [FromBody] CreateProductCategoryDto dto,
            CancellationToken ct)
        {
            var created = await _categoryAppService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public async Task<ActionResult<ProductCategoryDto>> Update(
            long id,
            [FromBody] UpdateProductCategoryDto dto,
            CancellationToken ct)
        {
            var updated = await _categoryAppService.UpdateAsync(id, dto, ct);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            await _categoryAppService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
