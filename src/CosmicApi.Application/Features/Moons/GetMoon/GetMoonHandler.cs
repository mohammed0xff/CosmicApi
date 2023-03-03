using MediatR;
using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Infrastructure.Context;

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
            var moons = _context.Moons;

            return await _mapper.ProjectTo<MoonResponse>(moons)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
