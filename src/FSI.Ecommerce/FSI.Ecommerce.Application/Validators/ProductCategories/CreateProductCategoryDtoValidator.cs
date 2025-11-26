using FluentValidation;
using FSI.Ecommerce.Application.Dtos.ProductCategories;

namespace FSI.Ecommerce.Application.Validators.ProductCategories
{
    public sealed class CreateProductCategoryDtoValidator
        : AbstractValidator<CreateProductCategoryDto>
    {
        public CreateProductCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}