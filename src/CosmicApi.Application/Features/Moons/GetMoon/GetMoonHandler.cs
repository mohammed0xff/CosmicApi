using MediatR;
using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Application.Extensions;

namespace CosmicApi.Application.Features.Moons.GetMoon
{
    public class GetMoonHandler : IRequestHandler<GetMoonRequest, PaginatedList<MoonResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetMoonHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<MoonResponse>> Handle(GetMoonRequest request, CancellationToken cancellationToken)
        {
            var moons = _context.Moons
                .WhereIf(!string.IsNullOrEmpty(request.Name),
                    m => m.Name.Contains(request.Name!))
                .WhereIf(request.PlanetId != null,
                    m => m.PlanetId.Equals(request.PlanetId));

            return await _mapper.ProjectTo<MoonResponse>(moons)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
