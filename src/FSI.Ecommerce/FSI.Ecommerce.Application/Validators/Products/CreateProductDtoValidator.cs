using FluentValidation;
using FSI.Ecommerce.Application.Dtos.Products;

namespace FSI.Ecommerce.Application.Validators.Products
{
    public sealed class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.InitialStockQuantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}