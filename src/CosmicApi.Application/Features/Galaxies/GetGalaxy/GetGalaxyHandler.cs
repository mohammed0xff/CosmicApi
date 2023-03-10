using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Extensions;
using CosmicApi.Infrastructure.Context;
using MediatR;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class GetGalaxyHandler : IRequestHandler<GetGalaxyRequest, PaginatedList<GalaxyResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetGalaxyHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<GalaxyResponse>> Handle(GetGalaxyRequest request, CancellationToken cancellationToken)
        {
            var galaxies = _context.Galaxies;
            galaxies.WhereIf(request.Type != null , x => x.Type.ToString() == request.Type);
                
            return await _mapper.ProjectTo<GalaxyResponse>( galaxies)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
