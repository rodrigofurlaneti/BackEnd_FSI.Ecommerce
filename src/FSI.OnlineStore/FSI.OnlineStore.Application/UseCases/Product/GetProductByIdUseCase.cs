using FSI.OnlineStore.Application.Dtos.Product;
using FSI.OnlineStore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Application.UseCases.Product
{
    public sealed class GetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse?> ExecuteAsync(uint productId, CancellationToken ct)
        {
            var product = await _productRepository.GetByIdAsync(productId, ct);
            if (product is null) return null;

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SkuCode = product.SkuCode,
                BasePrice = product.BasePrice,
                IsActive = product.IsActive
            };
        }
    }
}
