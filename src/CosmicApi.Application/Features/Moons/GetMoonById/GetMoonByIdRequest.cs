
using MediatR;

namespace CosmicApi.Application.Features.Moons.GetMoonById
{
    public record GetMoonByIdRequest(Guid Id) : IRequest<MoonResponse>;

}
