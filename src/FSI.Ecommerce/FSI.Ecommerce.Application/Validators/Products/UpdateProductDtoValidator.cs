using FluentValidation;
using FSI.Ecommerce.Application.Dtos.Products;

namespace FSI.Ecommerce.Application.Validators.Products
{
    public sealed class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3);
        }
    }
}