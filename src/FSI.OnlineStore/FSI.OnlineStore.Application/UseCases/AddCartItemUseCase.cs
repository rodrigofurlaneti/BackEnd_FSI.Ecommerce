using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FSI.OnlineStore.Application.Dtos;
using FSI.OnlineStore.Domain.Entities;
using FSI.OnlineStore.Domain.Repositories;

namespace FSI.OnlineStore.Application.UseCases
{
    public sealed class AddCartItemUseCase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public AddCartItemUseCase(
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        public async Task<ShoppingCart> ExecuteAsync(AddCartItemRequest request, CancellationToken ct)
        {
            if (!request.CustomerId.HasValue && string.IsNullOrWhiteSpace(request.VisitorToken))
            {
                throw new ArgumentException("CustomerId or VisitorToken must be provided.");
            }

            ShoppingCart? cart = null;

            if (request.CustomerId.HasValue)
            {
                cart = await _shoppingCartRepository.GetByCustomerAsync(request.CustomerId.Value, ct);
            }
            else if (!string.IsNullOrWhiteSpace(request.VisitorToken))
            {
                cart = await _shoppingCartRepository.GetByVisitorTokenAsync(request.VisitorToken, ct);
            }

            if (cart is null)
            {
                DateTime? expiresAt = request.CustomerId.HasValue
                    ? (DateTime?)null
                    : DateTime.UtcNow.AddDays(1);

                cart = new ShoppingCart(request.CustomerId, request.VisitorToken, expiresAt);
                var newId = await _shoppingCartRepository.InsertAsync(cart, ct);

                cart = await _shoppingCartRepository.GetByIdAsync(newId, ct)
                       ?? throw new InvalidOperationException("Cart not found after insert.");
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId, ct)
                          ?? throw new InvalidOperationException("Product not found.");

            var prices = await _productRepository.ListPricesByProductAsync(request.ProductId, ct);
            var unitPrice = ResolveUnitPrice(product, prices, request.Quantity);

            cart.AddOrUpdateItem(request.ProductId, request.Quantity, unitPrice);
            await _shoppingCartRepository.UpdateAsync(cart, ct);

            return cart;
        }

        private static decimal ResolveUnitPrice(
            Domain.Entities.Product product,
            IReadOnlyCollection<ProductPrice> prices,
            uint quantity)
        {
            var matching = prices.FirstOrDefault(p => p.IsInRange(quantity));
            return matching?.UnitPrice ?? product.BasePrice;
        }
    }
}
