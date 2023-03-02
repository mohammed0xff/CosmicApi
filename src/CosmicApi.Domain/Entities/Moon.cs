using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Moon : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Planet Planet { get; set; }
        public Guid PlanetId { get; set; }
    }
}
