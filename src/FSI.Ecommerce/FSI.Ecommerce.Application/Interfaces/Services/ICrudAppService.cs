using FSI.Ecommerce.Application.Dtos.Common;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface ICrudAppService<TReadDto, in TCreateDto, in TUpdateDto>
    {
        Task<PagedResultDto<TReadDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);

        Task<TReadDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<TReadDto> CreateAsync(TCreateDto dto, CancellationToken ct = default);

        Task<TReadDto?> UpdateAsync(long id, TUpdateDto dto, CancellationToken ct = default);

        Task DeleteAsync(long id, CancellationToken ct = default);
    }
}
