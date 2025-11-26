using FSI.Ecommerce.Application.Dtos.Products;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    public interface IProductAppService : ICrudAppService<ProductDto, CreateProductDto, UpdateProductDto>
    {
    }
}
