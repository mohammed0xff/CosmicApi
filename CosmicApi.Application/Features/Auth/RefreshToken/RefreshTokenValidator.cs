using FluentValidation;

namespace CosmicApi.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
