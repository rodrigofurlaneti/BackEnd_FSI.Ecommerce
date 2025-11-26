using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Dtos.Products;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Application.Interfaces.Services.Products
{
    public sealed class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductAppService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResultDto<ProductDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var items = await _productRepository.GetPagedAsync(pageNumber, pageSize, ct);

            var total = (await _productRepository.GetAllAsync(ct)).LongCount();

            var dtos = items.Select(MapToDto).ToList();

            return new PagedResultDto<ProductDto>(dtos, pageNumber, pageSize, total);
        }

        public async Task<ProductDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _productRepository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default)
        {
            var money = new Money(dto.Price, dto.Currency);
            var product = new Product(dto.Sku, dto.Name, money, dto.CategoryId);

            if (dto.InitialStockQuantity > 0)
            {
                product.IncreaseStock(dto.InitialStockQuantity);
            }

            await _productRepository.AddAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(product);
        }

        public async Task<ProductDto?> UpdateAsync(long id, UpdateProductDto dto, CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(id, ct);
            if (product is null)
                return null;

            var price = new Money(dto.Price, dto.Currency);

            product.ChangePrice(price);

            if (dto.IsActive && !product.IsActive)
                product.Activate();
            else if (!dto.IsActive && product.IsActive)
                product.Deactivate();

            product.GetType().GetProperty("Name")?.SetValue(product, dto.Name);
            product.GetType().GetProperty("Description")?.SetValue(product, dto.Description);
            product.GetType().GetProperty("CategoryId")?.SetValue(product, dto.CategoryId);

            await _productRepository.UpdateAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return MapToDto(product);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(id, ct);
            if (product is null)
                return;

            await _productRepository.DeleteAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.Amount,
                Currency = product.Price.Currency,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive
            };
        }
    }
}