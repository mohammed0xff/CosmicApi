using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities
{
    public class Picture : BaseEntity
    {
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public Guid LuminaryId { get; set; }
    }
}
