using MediatR;
using AutoMapper;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;


namespace CosmicApi.Application.Features.Stars.CreateStar
{
    public class CreateStarHandler : IRequestHandler<CreateStarRequest, StarResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public CreateStarHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StarResponse> Handle(CreateStarRequest request, CancellationToken cancellationToken)
        {
            var created = _mapper.Map<Star>(request);
            await _context.Stars.AddAsync(created);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<StarResponse>(created);
        }
    }
}
