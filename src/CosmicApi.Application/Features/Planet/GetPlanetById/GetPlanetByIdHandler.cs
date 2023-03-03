using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Extensions;
using CosmicApi.Application.Features.Planets;
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
            var star = await _context.Stars
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if(star == null) { return null; }
            
            return _mapper.Map<PlanetResponse?>(
                star
                );
        }
    }
}
