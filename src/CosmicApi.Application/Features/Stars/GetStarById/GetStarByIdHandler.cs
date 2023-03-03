using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Stars.GetStar
{
    public class GetStarByIdHandler : IRequestHandler<GetStarByIdRequest, StarResponse?>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetStarByIdHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StarResponse?> Handle(GetStarByIdRequest request, CancellationToken cancellationToken)
        {
            var star = await _context.Stars
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if(star == null) { return null; }
            
            return _mapper.Map<StarResponse>(star);
        }
    }
}
