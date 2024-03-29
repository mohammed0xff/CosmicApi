﻿using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Extensions;
using CosmicApi.Infrastructure.Context;
using MediatR;

namespace CosmicApi.Application.Features.Planets
{
    public class GetPlanetHandler : IRequestHandler<GetPlanetRequest, PaginatedList<PlanetResponse>>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetPlanetHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<PlanetResponse>> Handle(GetPlanetRequest request, CancellationToken cancellationToken)
        {
            var planets = _context.Planets
               .WhereIf(request.MaxNumberOfMoons != null, x => x.NumberOfMoons <= request.MaxNumberOfMoons)
               .WhereIf(request.MinNumberOfMoons != null, x => x.NumberOfMoons >= request.MinNumberOfMoons);

            return await _mapper.ProjectTo<PlanetResponse>(planets)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}
