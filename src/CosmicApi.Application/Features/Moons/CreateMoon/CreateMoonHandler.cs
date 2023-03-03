using MediatR;
using AutoMapper;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Moons.CreateMoon
{
    public class CreateMoonHandler : IRequestHandler<CreateMoonRequest, MoonResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public CreateMoonHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MoonResponse> Handle(CreateMoonRequest request, CancellationToken cancellationToken)
        {
            var created = _mapper.Map<Moon>(request);
            await _context.Moons.AddAsync(created);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MoonResponse>(created);
        }
    }
}
