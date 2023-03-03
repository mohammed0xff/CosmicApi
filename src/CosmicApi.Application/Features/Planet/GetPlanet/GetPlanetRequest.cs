using CosmicApi.Application.Common.Requests;
using CosmicApi.Application.Common.Responses;
using MediatR;


namespace CosmicApi.Application.Features.Planets
{
    public record GetPlanetRequest : PaginatedRequest, IRequest<PaginatedList<PlanetResponse>>
    {
        public int? MaxNumberOfMoons { get; set; }
        public int? MinNumberOfMoons { get; set; }
    }
}
