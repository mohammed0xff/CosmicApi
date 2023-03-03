using MediatR;

namespace CosmicApi.Application.Features.Pictures.GetPictures
{
    public record GetRandomPicturesRequest : IRequest<PictureResponse>;
}
