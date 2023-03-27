using CosmicApi.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public record UpdateGalaxyRequest : IRequest<GalaxyResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EscapeVelocity { get; set; }
        public string? AbsoluteMagnitude { get; set; }
        public string? Age { get; set; }
        public string? Radius { get; set; }
        public string? NumberOfStars { get; set; }
        public string Type { get; set; } = Enum.GetName(GalaxyType.other)!;
    }
}
