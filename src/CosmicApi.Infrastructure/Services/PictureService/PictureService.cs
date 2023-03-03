using CosmicApi.Domain.Entities;
using Microsoft.AspNetCore.Http;


namespace CosmicApi.Infrastructure.Services
{
    public class PictureService : IPictureService
    {
        private readonly string _SaveDirectory = "images";
        public Picture? UploadPicture(IFormFile File, string? name = null)
        {
            var allowedExtensions = new List<string>() { ".gif", ".png", ".jpeg", ".jpg" };
            if (!allowedExtensions.Contains(Path.GetExtension(File.FileName)))
                // todo : use result.error("File not supported.")
                return null;

            var imageId = Guid.NewGuid().ToString();
            if (!Directory.Exists(_SaveDirectory))
                Directory.CreateDirectory(_SaveDirectory);
            
            var fileName = imageId + name ?? File.FileName + Path.GetExtension(File.FileName);
            var diskFilePath = Path.Combine(_SaveDirectory, fileName);

            try
            {
                using var fileStream = new FileStream(diskFilePath, FileMode.Create);
                File.CopyTo(fileStream);
            }catch(Exception ex)
            {
                // todo : log ex
                return null;
            }

            return new Picture()
            {
                Name = fileName,
            };
        }
    }
}
