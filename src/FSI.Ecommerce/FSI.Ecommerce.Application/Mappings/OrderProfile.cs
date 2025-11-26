using AutoMapper;
using FSI.Ecommerce.Application.Dtos.Orders;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.UnitPrice,
                    opt => opt.MapFrom(src => src.UnitPrice.Amount))
                .ForMember(d => d.LineTotal,
                    opt => opt.MapFrom(src => src.LineTotal.Amount));

            CreateMap<Order, OrderSummaryDto>()
                .ForMember(d => d.TotalAmount,
                    opt => opt.MapFrom(src => src.TotalAmount.Amount))
                .ForMember(d => d.Currency,
                    opt => opt.MapFrom(src => src.TotalAmount.Currency));

            CreateMap<Order, OrderDetailDto>()
                .ForMember(d => d.TotalAmount,
                    opt => opt.MapFrom(src => src.TotalAmount.Amount))
                .ForMember(d => d.Currency,
                    opt => opt.MapFrom(src => src.TotalAmount.Currency))
                .ForMember(d => d.Items,
                    opt => opt.MapFrom(src => src.Items));
        }
    }
}