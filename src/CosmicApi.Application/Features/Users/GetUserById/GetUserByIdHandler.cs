using AutoMapper;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Application.Features.Users.GetUserById
{

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserResponse?>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserResponse?> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result is null) return null;

            return _mapper.Map<UserResponse>(result);
        }
    }
}