using MediatR;


namespace CosmicApi.Application.Features.Stars.GetStar
{
    public record GetStarByIdRequest(Guid Id) : IRequest<StarResponse>;

}
