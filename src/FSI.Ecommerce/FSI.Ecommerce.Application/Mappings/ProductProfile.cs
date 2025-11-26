using AutoMapper;
using FSI.Ecommerce.Application.Dtos.Products;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.ValueObjects;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Domain -> DTO
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .ForMember(d => d.Sku, opt => opt.MapFrom(s => s.Sku))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Price.Currency))
                .ForMember(d => d.StockQuantity, opt => opt.MapFrom(s => s.StockQuantity))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

            // Create DTO -> Domain (usa o construtor de 6 parâmetros)
            CreateMap<CreateProductDto, Product>()
                .ConstructUsing(dto =>
                    new Product(
                        dto.CategoryId,
                        dto.Sku,
                        dto.Name,
                        dto.Description,
                        Money.From(dto.Price, dto.Currency),
                        dto.InitialStockQuantity
                    ));

            // Update DTO -> Domain:
            // mapeamento via método de domínio, não via AutoMapper automático
            CreateMap<UpdateProductDto, Product>()
                .ForAllMembers(opt => opt.Ignore());
        }
    }
}
