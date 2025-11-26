using FSI.Ecommerce.Application.Dtos.Auth;
using FSI.Ecommerce.Application.Dtos.Common;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface IAuthAppService
    {
        Task<ResultDto<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
    }
}