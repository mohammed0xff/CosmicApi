using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Star : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? NumberOfPlanets { get; set; }

        public ICollection<Planet> Planets { get; set; }
        public Galaxy Galaxy { get; set; }
        public Guid GalaxyId { get; set; }

    }
}
