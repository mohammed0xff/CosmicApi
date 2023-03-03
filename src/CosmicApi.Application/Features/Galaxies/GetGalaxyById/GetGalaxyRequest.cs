using MediatR;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public record GetGalaxyrByIdRequest(Guid Id) : IRequest<GalaxyResponse>;
}
