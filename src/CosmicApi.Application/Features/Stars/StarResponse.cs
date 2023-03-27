namespace CosmicApi.Application.Features.Stars
{
    public class StarResponse 
    {
        public Guid StarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int NumberOfPlanets { get; set; }

    }
}
