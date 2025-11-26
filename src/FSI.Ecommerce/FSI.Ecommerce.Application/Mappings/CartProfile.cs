using AutoMapper;
using FSI.Ecommerce.Application.Dtos.Carts;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemDto>()
                .ForMember(d => d.ProductName,
                    opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));

            CreateMap<Cart, CartDto>()
                .ForMember(d => d.Items,
                    opt => opt.MapFrom(src => src.Items));
        }
    }
}