using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Star : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfMoons { get; set; }
        public float Age { get; set; }

        public ICollection<Planet> Planets { get; set; }
    }
}
