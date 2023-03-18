
using Ardalis.Result;
using CosmicApi.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CosmicApi.Infrastructure.Services
{
    public interface IPictureService
    {
        Result<Picture> UploadPicture(IFormFile File, string? name = null);
    }
}
