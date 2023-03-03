using CosmicApi.Application.Common.Requests;
using CosmicApi.Application.Common.Responses;
using MediatR;


namespace CosmicApi.Application.Features.Stars.GetStar
{
    public record GetStarRequest : PaginatedRequest, IRequest<PaginatedList<StarResponse>>
    {
        public int? MaxNumberOfPlanets { get; set; }
        public int? MinNumberOfPlanets { get; set; }
    }
}
