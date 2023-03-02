using AutoMapper;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace CosmicApi.Application.Features.Users.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, UserResponse>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public UpdatePasswordHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UserResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        // todo : get userid from claim principal (session)
        Guid userId = Guid.NewGuid();

        var user = await _context.Users
            .FirstAsync(x => x.Id == userId, cancellationToken);
        user.Password = BC.HashPassword(request.Password);
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }
}