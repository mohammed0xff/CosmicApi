using Ardalis.Result;
using CosmicApi.Application.Features.Users;
using MediatR;

namespace CosmicApi.Application.Features.Auth.Signup
{
    public record SignupRequest : IRequest<Result<UserResponse>>
    {
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}