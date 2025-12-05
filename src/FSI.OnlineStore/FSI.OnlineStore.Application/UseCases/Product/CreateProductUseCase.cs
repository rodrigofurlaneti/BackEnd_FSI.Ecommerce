using FSI.OnlineStore.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using FSI.OnlineStore.Application.Dtos.Product;
using FSI.OnlineStore.Domain;

namespace FSI.OnlineStore.Application.UseCases.Product
{
    public sealed class CreateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public CreateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<uint> ExecuteAsync(ProductCreateRequest request, CancellationToken ct)
        {
            var product = new Domain.Entities.Product(request.ProductName, request.SkuCode, request.BasePrice);
            var id = await _productRepository.InsertAsync(product, ct);
            return id;
        }
    }
}
