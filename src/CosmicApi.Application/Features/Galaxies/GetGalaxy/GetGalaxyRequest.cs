using CosmicApi.Application.Common.Requests;
using CosmicApi.Application.Common.Responses;
using MediatR;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public record GetGalaxyRequest : PaginatedRequest, IRequest<PaginatedList<GalaxyResponse>>
    {
        public string? Type { get; set; }
    }
}
