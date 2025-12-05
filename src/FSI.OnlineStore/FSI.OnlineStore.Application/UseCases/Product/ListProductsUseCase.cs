using FSI.OnlineStore.Application.Dtos.Product;
using FSI.OnlineStore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Application.UseCases.Product
{
    public sealed class ListProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public ListProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<ProductResponse>> ExecuteAsync(CancellationToken ct)
        {
            var products = await _productRepository.ListAsync(ct);

            var response = products
                .Select(p => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    SkuCode = p.SkuCode,
                    BasePrice = p.BasePrice,
                    IsActive = p.IsActive
                })
                .ToList()
                .AsReadOnly();

            return response;
        }
    }
}