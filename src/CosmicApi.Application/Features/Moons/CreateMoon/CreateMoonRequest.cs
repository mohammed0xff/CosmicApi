using MediatR;

namespace CosmicApi.Application.Features.Moons.CreateMoon
{ 
    public record CreateMoonRequest :IRequest<MoonResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid PlanetId { get; set; }
    }
}
