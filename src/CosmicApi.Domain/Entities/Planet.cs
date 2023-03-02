using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Planet : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfMoons { get; set; } = 0;

        public ICollection<Moon> Moons { get; set; }
        public Star Star { get; set; }
        public Galaxy Galaxy { get; set; }
        public Guid GalaxyId { get; set; }
        public Guid StarId { get; set; }

    }
}
