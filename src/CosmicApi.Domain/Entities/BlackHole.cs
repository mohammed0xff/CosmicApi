using CosmicApi.Domain.Entities.Common;

namespace CosmicApi.Domain.Entities
{
    public class BlackHole : BaseEntity
    {
        public string Name { get; init; }
        public string Description { get; set; }
        public Galaxy Galaxy { get; private init; }
        public Guid GalaxyId { get; set; }

    }
}
