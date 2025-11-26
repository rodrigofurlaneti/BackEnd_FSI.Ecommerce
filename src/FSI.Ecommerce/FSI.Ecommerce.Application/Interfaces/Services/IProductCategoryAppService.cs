using FSI.Ecommerce.Application.Dtos.ProductCategories;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface IProductCategoryAppService : ICrudAppService<ProductCategoryDto, CreateProductCategoryDto, UpdateProductCategoryDto>
    {
    }
}