
using CosmicApi.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CosmicApi.Infrastructure.Services
{
    public interface IPictureService
    {
        Picture? UploadPicture(IFormFile File, string? name = null);
    }
}
