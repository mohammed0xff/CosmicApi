using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Extensions;
using CosmicApi.Infrastructure.Context;


namespace CosmicApi.Application.Features.Users.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedList<UserResponse>>
    {
        private readonly IContext _context;

        private readonly IMapper _mapper;

        public GetUsersHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<UserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = _context.Users
                .WhereIf(!string.IsNullOrEmpty(request.Email),
                    x => EF.Functions.Like(x.Email, $"%{request.Email}%"));

            return await _mapper.ProjectTo<UserResponse>(users)
                .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }
    }
}