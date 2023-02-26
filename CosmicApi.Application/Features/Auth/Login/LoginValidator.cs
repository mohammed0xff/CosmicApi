using FluentValidation;

namespace CosmicApi.Application.Features.Auth.Authenticate;

public class LoginValidator : AbstractValidator<LoginRequest>
{

    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}