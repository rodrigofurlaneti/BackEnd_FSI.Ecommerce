using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.ProductCategories;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Application.Interfaces.Services.ProductCategories
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

        public async Task<PagedResultDto<ProductCategoryDto>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var items = await _categoryRepository.GetPagedAsync(pageNumber, pageSize, ct);
            var total = (await _categoryRepository.GetAllAsync(ct)).LongCount();

            var dtos = items.Select(MapToDto).ToList();

            return new PagedResultDto<ProductCategoryDto>(dtos, pageNumber, pageSize, total);
        }

        public async Task<ProductCategoryDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<ProductCategoryDto> CreateAsync(ProductCategoryDto dto, CancellationToken ct = default)
        {
            var entity = new ProductCategory(dto.Name, dto.Slug, dto.ParentId);
            await _categoryRepository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return MapToDto(entity);
        }

        public async Task<ProductCategoryDto?> UpdateAsync(long id, ProductCategoryDto dto, CancellationToken ct = default)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            if (entity is null)
                return null;

            entity.GetType().GetProperty("Name")?.SetValue(entity, dto.Name);
            entity.GetType().GetProperty("Slug")?.SetValue(entity, dto.Slug);
            entity.GetType().GetProperty("ParentId")?.SetValue(entity, dto.ParentId);

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