using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities{

    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }
        public RefreshToken RefreshToken { get; set; } = null!;
    }
}