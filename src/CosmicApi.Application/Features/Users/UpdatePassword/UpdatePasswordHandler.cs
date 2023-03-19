using AutoMapper;
using CosmicApi.Application.Common.Session;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace CosmicApi.Application.Features.Users.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, UserResponse>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;
    private readonly ISession _session;

    public UpdatePasswordHandler(IMapper mapper, IContext context, ISession session)
    {
        _mapper = mapper;
        _context = context;
        _session = session;
    }

    public async Task<UserResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _session.UserId;

        var user = await _context.Users
            .FirstAsync(x => x.Id == userId, cancellationToken);
        user.Password = BC.HashPassword(request.Password);
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }
}