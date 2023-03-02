using CosmicApi.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmicApi.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
