using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.Orders;
using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Enums;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Domain.Services;
using FSI.Ecommerce.Domain.ValueObjects;

namespace FSI.Ecommerce.Application.Interfaces.Services.Orders
{
    public sealed class OrderAppService : IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderAppService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IAccountRepository accountRepository,
            IOrderDomainService orderDomainService,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _accountRepository = accountRepository;
            _orderDomainService = orderDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetailDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByIdAsync(id, ct);
            return order is null ? null : MapToDetailDto(order);
        }

        public async Task<OrderDetailDto?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default)
        {
            var order = await _orderRepository.GetByOrderNumberAsync(orderNumber, ct);
            return order is null ? null : MapToDetailDto(order);
        }

        public async Task<PagedResultDto<OrderSummaryDto>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var items = await _orderRepository.GetPagedAsync(pageNumber, pageSize, ct);
            var total = (await _orderRepository.GetAllAsync(ct)).LongCount();

            var summaries = items.Select(MapToSummaryDto).ToList();

            return new PagedResultDto<OrderSummaryDto>(summaries, pageNumber, pageSize, total);
        }

        public async Task<OrderDetailDto> PlaceOrderFromCartForAccountAsync(
            long accountId,
            long? userId,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByAccountIdAsync(accountId, ct)
                       ?? throw new InvalidOperationException("Open cart not found.");

            var account = await _accountRepository.GetByIdAsync(accountId, ct)
                          ?? throw new InvalidOperationException("Account not found.");

            var order = _orderDomainService.CreateOrderFromCart(cart, account, userId);

            await _orderRepository.AddAsync(order, ct);

            cart.MarkConverted();
            await _cartRepository.UpdateAsync(cart, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDetailDto(order);
        }

        public async Task<OrderDetailDto> PlaceOrderFromCartForGuestAsync(
            string guestToken,
            CancellationToken ct = default)
        {
            var cart = await _cartRepository.GetOpenCartByGuestTokenAsync(guestToken, ct)
                       ?? throw new InvalidOperationException("Open cart not found.");

            throw new NotImplementedException("Guest order placement depends on guest-to-account flow.");
        }

        private static OrderSummaryDto MapToSummaryDto(Order order)
        {
            return new OrderSummaryDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                TotalAmount = order.TotalAmount.Amount,
                Currency = order.TotalAmount.Currency,
                CreatedAt = order.CreatedAt
            };
        }

        private static OrderDetailDto MapToDetailDto(Order order)
        {
            return new OrderDetailDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                AccountId = order.AccountId,
                PlacedByUserId = order.PlacedByUserId,
                Status = order.Status,
                TotalAmount = order.TotalAmount.Amount,
                Currency = order.TotalAmount.Currency,
                CreatedAt = order.CreatedAt,
                ShippingName = order.ShippingName,
                ShippingLine1 = order.ShippingLine1,
                ShippingLine2 = order.ShippingLine2,
                ShippingCity = order.ShippingCity,
                ShippingState = order.ShippingState,
                ShippingPostalCode = order.ShippingPostalCode,
                ShippingCountryCode = order.ShippingCountryCode,
                BillingName = order.BillingName,
                BillingLine1 = order.BillingLine1,
                BillingLine2 = order.BillingLine2,
                BillingCity = order.BillingCity,
                BillingState = order.BillingState,
                BillingPostalCode = order.BillingPostalCode,
                BillingCountryCode = order.BillingCountryCode,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice.Amount,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal.Amount
                }).ToList()
            };
        }
    }
}