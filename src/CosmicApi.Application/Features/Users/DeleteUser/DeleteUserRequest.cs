using MediatR;


namespace CosmicApi.Application.Features.Users.DeleteUser
{
    public record DeleteUserRequest(Guid Id) : IRequest<bool>;
}
