using FluentValidation;
using FSI.Ecommerce.Application.Dtos.Carts;

namespace FSI.Ecommerce.Application.Validators.Carts
{
    public sealed class AddCartItemRequestDtoValidator
        : AbstractValidator<AddCartItemRequestDto>
    {
        public AddCartItemRequestDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}