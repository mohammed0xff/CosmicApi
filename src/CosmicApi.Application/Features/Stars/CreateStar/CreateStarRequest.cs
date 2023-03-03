using MediatR;

namespace CosmicApi.Application.Features.Stars.CreateStar
{
    public record CreateStarRequest : IRequest<StarResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? NumberOfPlanets { get; set; }
        public float? Age { get; set; }
        public Guid GalaxyId { get; set; }
    }
}
