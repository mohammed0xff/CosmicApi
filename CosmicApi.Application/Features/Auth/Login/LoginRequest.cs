using Ardalis.Result;
using CosmicApi.Infrastructure.Common;
using MediatR;

namespace CosmicApi.Application.Features.Auth.Authenticate;

public record LoginRequest : IRequest<Result<Jwt>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; }  = null!;
}