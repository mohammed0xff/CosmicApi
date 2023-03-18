using Ardalis.Result;
using CosmicApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CosmicApi.Infrastructure.Services
{
    public class PictureService : IPictureService
    {
        private readonly string _saveDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Pictures");
        public PictureService(ILogger logger)
        {
            _logger= logger;
        }
        private ILogger _logger;
        public Result<Picture> UploadPicture(IFormFile File, string? name = null)
        {
            var allowedExtensions = new List<string>() { ".gif", ".png", ".jpeg", ".jpg" };
            if (!allowedExtensions.Contains(Path.GetExtension(File.FileName)))
                return Result.Error("File not supported.");

            var imageId = Guid.NewGuid().ToString();
            
            var fileName = ( name ?? File.FileName ) + imageId + Path.GetExtension(File.FileName);
            var diskFilePath = Path.Combine(_saveDirectoryPath, fileName);

            try
            {
                using var fileStream = new FileStream(diskFilePath, FileMode.Create);
                File.CopyTo(fileStream);
            }catch(Exception ex)
            {
                _logger.LogError($"Uploading picture with filename: {fileName}, Exception: {ex.Message}");
                return Result.Error("Something went wrong.");
            }

            return Result.Success( 
                new Picture(){ Name = fileName }
                );
        }
    }
}
