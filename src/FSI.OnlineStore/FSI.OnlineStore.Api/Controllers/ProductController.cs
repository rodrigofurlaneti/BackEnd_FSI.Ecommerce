using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSI.OnlineStore.Application.Dtos.Product;
using FSI.OnlineStore.Application.UseCases.Product;

namespace FSI.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProductController : ControllerBase
    {
        private readonly CreateProductUseCase _createProductUseCase;
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        private readonly ListProductsUseCase _listProductsUseCase;

        public ProductController(
            CreateProductUseCase createProductUseCase,
            GetProductByIdUseCase getProductByIdUseCase,
            ListProductsUseCase listProductsUseCase)
        {
            _createProductUseCase = createProductUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _listProductsUseCase = listProductsUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ProductCreateRequest request,
            CancellationToken ct)
        {
            var id = await _createProductUseCase.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { ProductId = id });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(uint id, CancellationToken ct)
        {
            var product = await _getProductByIdUseCase.ExecuteAsync(id, ct);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> List(CancellationToken ct)
        {
            var products = await _listProductsUseCase.ExecuteAsync(ct);
            return Ok(products);
        }
    }
}
