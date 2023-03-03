namespace CosmicApi.Application.Features.Users
{
    public record UserResponse
    {
        public Guid Id { get; init; }
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}