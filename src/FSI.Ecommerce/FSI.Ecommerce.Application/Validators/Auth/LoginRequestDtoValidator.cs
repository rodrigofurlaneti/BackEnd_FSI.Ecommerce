using FluentValidation;
using FSI.Ecommerce.Application.Dtos.Auth;

namespace FSI.Ecommerce.Application.Validators.Auth
{
    public sealed class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}