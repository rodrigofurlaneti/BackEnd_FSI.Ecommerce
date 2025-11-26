using FSI.Ecommerce.Application.Dtos.Carts;
using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Application.Interfaces.Services.Carts
{
    public sealed class CartAppService : ICartAppService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartDomainService _cartDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CartAppService(
            ICartRepository cartRepository,
            IProductRepository productRepository,
            ICartDomainService cartDomainService,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartDomainService = cartDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartDto> GetOrCreateCartForAccountAsync(
            long accountId,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByAccountIdAsync(accountId, ct)
                       ?? await CreateCartForAccountAsync(accountId, ct);

            return MapToDto(cart);
        }

        public async Task<CartDto> GetOrCreateCartForGuestAsync(
            string guestToken,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByGuestTokenAsync(guestToken, ct)
                       ?? await CreateCartForGuestAsync(guestToken, ct);

            return MapToDto(cart);
        }

        public async Task<CartDto> AddItemForAccountAsync(
            long accountId,
            AddCartItemRequestDto request,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByAccountIdAsync(accountId, ct)
                       ?? await CreateCartForAccountAsync(accountId, ct);

            var product = await _productRepository.GetByIdAsync(request.ProductId, ct)
                          ?? throw new InvalidOperationException("Product not found.");

            _cartDomainService.AddOrUpdateItem(cart, product.Id, request.Quantity, product.Price.Amount);

            await _cartRepository.UpdateAsync(cart, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(cart);
        }

        public async Task<CartDto> AddItemForGuestAsync(
            string guestToken,
            AddCartItemRequestDto request,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByGuestTokenAsync(guestToken, ct)
                       ?? await CreateCartForGuestAsync(guestToken, ct);

            var product = await _productRepository.GetByIdAsync(request.ProductId, ct)
                          ?? throw new InvalidOperationException("Product not found.");

            _cartDomainService.AddOrUpdateItem(cart, product.Id, request.Quantity, product.Price.Amount);

            await _cartRepository.UpdateAsync(cart, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(cart);
        }

        private async Task<Cart> CreateCartForAccountAsync(long accountId, CancellationToken ct)
        {
            var cart = new Cart(accountId, null);
            await _cartRepository.AddAsync(cart, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return cart;
        }

        private async Task<Cart> CreateCartForGuestAsync(string guestToken, CancellationToken ct)
        {
            var cart = new Cart(null, guestToken);
            await _cartRepository.AddAsync(cart, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return cart;
        }

        private static CartDto MapToDto(Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                AccountId = cart.AccountId,
                GuestToken = cart.GuestToken,
                Status = cart.Status,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? string.Empty,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }
    }
}