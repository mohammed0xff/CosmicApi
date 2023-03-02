using CosmicApi.Domain.Entities.Common;
using MediatR;

namespace CosmicApi.Application.Features.Users.GetUserById
{
    public record GetUserByIdRequest(Guid Id) : IRequest<UserResponse?>;
}