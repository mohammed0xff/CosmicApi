using CosmicApi.Application.Features.Planets;
using MediatR;


namespace CosmicApi.Application.Features.Planets
{
    public record GetPlanetByIdRequest(Guid Id) : IRequest<PlanetResponse>;

}
