using CosmicApi.Domain.Entities.Common;
using CosmicApi.Domain.Entities.Enums;


namespace CosmicApi.Domain.Entities
{
    public class Galaxy : BaseEntity
    {
        public string Name { get; private init; }
        public string? Description { get; set; }
        public int? NumberOfStars { get; set; }
        public float? Age { get; set; }
        public GalaxyType Type { get; private init; } = GalaxyType.Other;

        public ICollection<Star> Stars { get; set; }
        public ICollection<Planet> Planets { get; set; }
        public ICollection<BlackHole> BlackHoles { get; set; }

    }
}
