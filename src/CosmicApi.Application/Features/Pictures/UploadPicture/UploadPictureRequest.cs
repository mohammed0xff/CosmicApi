using MediatR;
using Microsoft.AspNetCore.Http;

namespace CosmicApi.Application.Features.Pictures.UploadPicture
{
    public record UploadPictureRequest : IRequest<PictureResponse?>
    {
        public string? Name { get; set; }
        public IFormFile FormFile { get; set; }
        public Guid LuminaryId { get; set; }
    }
}
