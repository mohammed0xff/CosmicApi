using CosmicApi.Domain.Entities.Common;
using CosmicApi.Domain.Entities.Enums;
using static System.Formats.Asn1.AsnWriter;


namespace CosmicApi.Domain.Entities
{
    public class Galaxy : BaseEntity
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; set; }
        public string? EscapeVelocity { get; set; }
        public string? AbsoluteMagnitude { get; set; }
        public string? Age { get; set; }
        public string? Radius { get; set; }
        public string? NumberOfStars { get; set; }

        public GalaxyType Type { get; set; } = GalaxyType.Other;

        public ICollection<Star> Stars { get; set; }
        public ICollection<Planet> Planets { get; set; }
        public ICollection<BlackHole> BlackHoles { get; set; }

    }
}
