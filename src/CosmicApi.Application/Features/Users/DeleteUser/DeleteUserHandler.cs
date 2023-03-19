using CosmicApi.Application.Common.Exceptions;
using CosmicApi.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Application.Features.Users.DeleteUser
{

    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IContext _context;

        public DeleteUserHandler(IContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null)
                throw new UserNotFoundException(request.Id);
            _context.Users.Remove(user!);
            
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}