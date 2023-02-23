using CosmicApi.Domain.Entities.Common;


namespace CosmicApi.Domain.Entities{

    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}