using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CosmicApi.Application.Features.Pictures.UploadPicture
{
    public record UploadPictureRequest : IRequest<Result<PictureResponse>>
    {
        public string? Name { get; set; }
        public IFormFile FormFile { get; set; }
        public Guid LuminaryId { get; set; }
        public string Description { get; set; } = null!;
    }
}
