using AutoMapper;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class UpdateGalaxyHandler : IRequestHandler<UpdateGalaxyRequest, GalaxyResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public UpdateGalaxyHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GalaxyResponse> Handle(UpdateGalaxyRequest request, CancellationToken cancellationToken)
        {
            var galaxy = await _context.Galaxies
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            if (galaxy == null)
                return null;

            galaxy.Description = request.Description;
            
            _context.Galaxies.Update(galaxy);   
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GalaxyResponse>(galaxy);
        }
    }
}
