using CosmicApi.Application.Common.Requests;
using CosmicApi.Application.Common.Responses;
using MediatR;


namespace CosmicApi.Application.Features.Moons.GetMoon
{
    public record GetMoonRequest : PaginatedRequest, IRequest<PaginatedList<MoonResponse>>;
}
