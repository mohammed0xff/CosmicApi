using MediatR;
using Ardalis.Result;
using CosmicApi.Infrastructure.Common;
using CosmicApi.Infrastructure.Services.TokenService;

namespace CosmicApi.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<Infrastructure.Common.RefreshTokenResponse>>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
           return await _tokenService.RefreshToken(request.Token, request.RefreshToken);
        }
    }
}
