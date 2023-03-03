using FluentValidation;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class DeleteGalaxyValidator : AbstractValidator<DeleteGalaxyRequest>
    {
        public DeleteGalaxyValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
