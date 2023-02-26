using MediatR;
using Microsoft.EntityFrameworkCore;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Infrastructure.Services;
using CosmicApi.Infrastructure.Common;
using BC = BCrypt.Net.BCrypt;
using Ardalis.Result;

namespace CosmicApi.Application.Features.Auth.Authenticate
{
    public class LoginHandler : IRequestHandler<LoginRequest, Result<Jwt>>
    {
        private readonly IContext _context;
        private readonly ITokenService _tokenService;

        public LoginHandler(IContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Result<Jwt>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
            if (user == null || !BC.Verify(request.Password, user.Password))
            {
                return null;
            }
            return await _tokenService.GenerateAccessToken(user);

        }
    }
}