using MediatR;
using CosmicApi.Infrastructure.Services;
using Ardalis.Result;
using CosmicApi.Infrastructure.Common;


namespace CosmicApi.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<Jwt>>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;

        }
        public async Task<Result<Jwt>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
           return await _tokenService.GenerateRefreshToken(request.Token);
        }
    }
}
