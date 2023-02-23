using CosmicApi.Domain.Entities.Common;

namespace CosmicApi.Domain.Entities
{
    public class BlackHole : BaseEntity
    {
        public string Name { get; private init; }
        public string Description { get; set; }
    }
}
