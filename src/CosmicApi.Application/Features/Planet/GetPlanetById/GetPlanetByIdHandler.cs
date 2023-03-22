using AutoMapper;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Application.Features.Planets
{
    public class GetPlanetByIdHandler : IRequestHandler<GetPlanetByIdRequest, PlanetResponse?>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetPlanetByIdHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlanetResponse?> Handle(GetPlanetByIdRequest request, CancellationToken cancellationToken)
        {
            var planet = await _context.Planets
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if(planet == null) { return null; }
            
            return _mapper.Map<PlanetResponse?>(
                planet
                );
        }
    }
}
