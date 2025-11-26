using FluentValidation;
using FSI.Ecommerce.Application.Dtos.ProductCategories;

namespace FSI.Ecommerce.Application.Validators.ProductCategories
{
    public sealed class UpdateProductCategoryDtoValidator
       : AbstractValidator<UpdateProductCategoryDto>
    {
        public UpdateProductCategoryDtoValidator()
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