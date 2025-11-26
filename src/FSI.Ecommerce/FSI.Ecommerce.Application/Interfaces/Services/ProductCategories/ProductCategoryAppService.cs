using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.ProductCategories;
using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Application.Services
{
    public sealed class ProductCategoryAppService : IProductCategoryAppService
    {
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryAppService(
            IRepository<ProductCategory> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ProductCategoryDto>> GetAllAsync(
            CancellationToken ct = default)
        {
            var entities = await _categoryRepository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PagedResultDto<ProductCategoryDto>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var entitiesPaged = await _categoryRepository.GetPagedAsync(pageNumber, pageSize, ct);
            var all = await _categoryRepository.GetAllAsync(ct);
            var total = all.LongCount();

            var dtos = entitiesPaged.Select(MapToDto).ToList();

            return new PagedResultDto<ProductCategoryDto>(
                dtos,
                pageNumber,
                pageSize,
                total
            );
        }

        public async Task<ProductCategoryDto?> GetByIdAsync(
            long id,
            CancellationToken ct = default)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<ProductCategoryDto> CreateAsync(
            CreateProductCategoryDto dto,
            CancellationToken ct = default)
        {
            var entity = new ProductCategory(dto.Name, dto.Slug, dto.ParentId);

            await _categoryRepository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(entity);
        }

        public async Task<ProductCategoryDto?> UpdateAsync(
            long id,
            UpdateProductCategoryDto dto,
            CancellationToken ct = default)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            if (entity is null)
                return null;

            entity.Update(dto.Name, dto.Slug, dto.ParentId);

            await _categoryRepository.UpdateAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(entity);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            if (entity is null)
                return;

            await _categoryRepository.DeleteAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        private static ProductCategoryDto MapToDto(ProductCategory entity)
        {
            return new ProductCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                ParentId = entity.ParentId
            };
        }
    }
}
