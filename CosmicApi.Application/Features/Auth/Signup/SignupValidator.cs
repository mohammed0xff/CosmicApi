using FluentValidation;

namespace CosmicApi.Application.Features.Auth.Signup
{

    public class SignupValidator : AbstractValidator<SignupRequest>
    {
        public SignupValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(254)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(255);

        }
    }
}