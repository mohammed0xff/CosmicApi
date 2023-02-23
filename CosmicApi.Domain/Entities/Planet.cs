using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Planet : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfMoons { get; set; }
        public float Age { get; set; }

        public ICollection<Moon> Moons { get; set; }
    }
}
