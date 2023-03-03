using CosmicApi.Application.Common.Requests;
using CosmicApi.Application.Common.Responses;
using MediatR;

namespace CosmicApi.Application.Features.Pictures.GetPictures
{
    public record GetPicturesRequest : PaginatedRequest, IRequest<PaginatedList<PictureResponse>>
    {
        public bool New { get; set; } = false;
    }
    public record GetLuminaryPicturesRequest : GetPicturesRequest
    {
        public GetLuminaryPicturesRequest(GetPicturesRequest request) : base(request) {}
        public Guid? LuminaryId { get; set; }
    }
}

