using MediatR;
using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Extensions;
using CosmicApi.Infrastructure.Context;

namespace CosmicApi.Application.Features.Stars.GetStar
{
    public class GetStarHandler : IRequestHandler<GetStarRequest, PaginatedList<StarResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetStarHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<StarResponse>> Handle(GetStarRequest request, CancellationToken cancellationToken)
        {
            var stars = _context.Stars
               .WhereIf(request.MaxNumberOfPlanets != null, x => x.NumberOfPlanets <= request.MaxNumberOfPlanets)
               .WhereIf(request.MinNumberOfPlanets != null, x => x.NumberOfPlanets >= request.MinNumberOfPlanets);

            return await _mapper.ProjectTo<StarResponse>(stars)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
