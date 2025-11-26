using AutoMapper;
using FSI.Ecommerce.Application.Dtos.ProductCategories;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<CreateProductCategoryDto, ProductCategory>()
                .ConstructUsing(src => new ProductCategory(
                    src.Name,
                    src.Slug,
                    src.ParentId));
        }
    }
}