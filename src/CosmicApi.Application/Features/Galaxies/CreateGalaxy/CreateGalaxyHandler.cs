using AutoMapper;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using MediatR;


namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class CreateGalaxyHandler : IRequestHandler<CreateGalaxyRequest, GalaxyResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public CreateGalaxyHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GalaxyResponse> Handle(CreateGalaxyRequest request, CancellationToken cancellationToken)
        {
            var created = _mapper.Map<Galaxy>(request);
            await _context.Galaxies.AddAsync(created);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GalaxyResponse>(created);
        }
    }
}
