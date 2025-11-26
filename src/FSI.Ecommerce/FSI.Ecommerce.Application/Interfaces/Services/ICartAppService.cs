using FSI.Ecommerce.Application.Dtos.Carts;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface ICartAppService
    {
        Task<CartDto> GetOrCreateCartForAccountAsync(
            long accountId,
            CancellationToken ct = default);

        Task<CartDto> GetOrCreateCartForGuestAsync(
            string guestToken,
            CancellationToken ct = default);

        Task<CartDto> AddItemForAccountAsync(
            long accountId,
            AddCartItemRequestDto request,
            CancellationToken ct = default);

        Task<CartDto> AddItemForGuestAsync(
            string guestToken,
            AddCartItemRequestDto request,
            CancellationToken ct = default);
    }
}