using FluentValidation;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class CreateGalaxyValidator : AbstractValidator<CreateGalaxyRequest>
    {
        public CreateGalaxyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
