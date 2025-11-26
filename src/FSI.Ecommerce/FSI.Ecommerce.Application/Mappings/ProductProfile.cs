using AutoMapper;
using FSI.Ecommerce.Application.Dtos.Products;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Domain -> DTO
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Price,
                    opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(d => d.Currency,
                    opt => opt.MapFrom(src => src.Price.Currency));

            // DTO -> Domain (create)
            CreateMap<CreateProductDto, Product>()
                .ConstructUsing(src =>
                    new Product(
                        src.Sku,
                        src.Name,
                        new Money(src.Price, src.Currency),
                        src.CategoryId));
        }
    }
}