using FluentValidation;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class UpdateGalaxyValidator : AbstractValidator<UpdateGalaxyRequest>
    {
        public UpdateGalaxyValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
