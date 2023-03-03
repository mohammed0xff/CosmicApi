using CosmicApi.Application.Common.Requests;
namespace CosmicApi.Application.Features.Moons
{
    public record MoonResponse 
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid PlanetId { get; set; }
    }
}
