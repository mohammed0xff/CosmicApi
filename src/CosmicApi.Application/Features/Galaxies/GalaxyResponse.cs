using CosmicApi.Domain.Entities.Enums;

namespace CosmicApi.Application.Features.Galaxies
{
    public class GalaxyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EscapeVelocity { get; set; }
        public string? AbsoluteMagnitude { get; set; }
        public string? Age { get; set; }
        public string? Radius { get; set; }
        public string? NumberOfStars { get; set; }
        public string? Type { get; set; } 

    }
}
