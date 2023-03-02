using MediatR;

namespace CosmicApi.Application.Features.Users.UpdatePassword;

public record UpdatePasswordRequest : IRequest<UserResponse>
{
    public string Password { get; init; } = null!;
}