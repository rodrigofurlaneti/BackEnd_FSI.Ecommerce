using AutoMapper;
using FSI.Ecommerce.Application.Dtos.Payments;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Mappings
{
    public sealed class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentTransaction, PaymentTransactionDto>()
                .ForMember(d => d.Amount,
                    opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(d => d.Currency,
                    opt => opt.MapFrom(src => src.Amount.Currency));
        }
    }
}