using MediatR;
using AutoMapper;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Planets.CreatePlanet
{
    public class CreatePlanetHandler : IRequestHandler<CreatePlanetRequest, PlanetResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public CreatePlanetHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PlanetResponse> Handle(CreatePlanetRequest request, CancellationToken cancellationToken)
        {
            var created = _mapper.Map<Planet>(request);
            await _context.Planets.AddAsync(created);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PlanetResponse>(created);
        }
    }
}
