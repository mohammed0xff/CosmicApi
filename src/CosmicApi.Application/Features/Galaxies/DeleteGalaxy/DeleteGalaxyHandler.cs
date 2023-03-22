using AutoMapper;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class DeleteGalaxyHandler : IRequestHandler<DeleteGalaxyRequest, bool>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public DeleteGalaxyHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteGalaxyRequest request, CancellationToken cancellationToken)
        {
            var galaxy = await _context.Galaxies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (galaxy is null)
                return false;    
            
            _context.Galaxies.Remove(galaxy!);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
         }
    }

}
