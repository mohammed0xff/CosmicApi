using CosmicApi.Domain.Entities;
using MediatR;


namespace CosmicApi.Application.Features.Planets.CreatePlanet
{
    public record CreatePlanetRequest : IRequest<PlanetResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfMoons { get; set; } = 0;

        public Guid GalaxyId { get; set; }
        public Guid StarId { get; set; }
    }
}
