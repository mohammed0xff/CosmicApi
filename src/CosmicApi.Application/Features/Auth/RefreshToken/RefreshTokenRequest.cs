using MediatR;
using Ardalis.Result;
using CosmicApi.Infrastructure.Common;

namespace CosmicApi.Application.Features.Auth.RefreshToken
{
    public record RefreshTokenRequest :IRequest<Result<RefreshTokenResponse>>
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; } 
    }
}
