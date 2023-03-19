using CosmicApi.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace CosmicApi.Application.Features.Pictures
{
    public class PictureResponse
    {
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string Description { get; set; } = string.Empty;

        [Url]
        public string URL { get; set; }
        public DateTime AddedAt { get; set; }

    }
}
