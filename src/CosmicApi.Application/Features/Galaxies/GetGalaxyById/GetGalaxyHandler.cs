using AutoMapper;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicApi.Application.Features.Galaxies.CreateGalaxy
{
    public class GetGalaxyByIdHandler : IRequestHandler<GetGalaxyrByIdRequest, GalaxyResponse>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetGalaxyByIdHandler(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GalaxyResponse> Handle(GetGalaxyrByIdRequest request, CancellationToken cancellationToken)
        {
            var galaxy = await _context.Galaxies
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            return _mapper.Map<GalaxyResponse>(
                galaxy
                );
        }
    }
}
