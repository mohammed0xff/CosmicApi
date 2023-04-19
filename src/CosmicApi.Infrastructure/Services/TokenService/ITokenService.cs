using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Common;
using Ardalis.Result;


namespace CosmicApi.Infrastructure.Services.TokenService
{
    public interface ITokenService
    {
        Task<RefreshTokenResponse> GenerateAccessToken(User user);
        Task<Result<RefreshTokenResponse>> RefreshToken(string token, string refreshToken);
    }
}
