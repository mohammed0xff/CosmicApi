namespace CosmicApi.Infrastructure.Common
{
    public record RefreshTokenResponse
    {
        public string Token { get; init; } = null!;
        public DateTime ExpDate { get; init; }
        public string RefreshToken { get; init; } = null!;
    }
}