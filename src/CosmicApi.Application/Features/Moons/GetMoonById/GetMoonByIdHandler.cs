using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Moons.GetMoonById
{
    public class GetMoonByIdHandler : IRequestHandler<GetMoonByIdRequest, MoonResponse?>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetMoonByIdHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MoonResponse?> Handle(GetMoonByIdRequest request, CancellationToken cancellationToken)
        {
            var moon = await _context.Stars
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if(moon == null) { return null; }
            
            return _mapper.Map<MoonResponse?>(
                moon
                );
        }
    }
}
