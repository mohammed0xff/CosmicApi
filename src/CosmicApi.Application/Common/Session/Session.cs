using System.Security.Claims;
using CosmicApi.Domain.Constants;

namespace CosmicApi.Application.Common.Session
{
    public class Session : ISession
    {
        private readonly ClaimsPrincipal _user;

        public DateTime Now => DateTime.Now;
        public Guid UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }

        public Session(ClaimsPrincipal user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));

            IsAuthenticated = _user.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticated)
            {
                UserId = GetUserId();
                Email = GetClaimValue(ClaimTypes.Email);
                Username = GetClaimValue("username");
                IsAdmin = _user.IsInRole(Roles.Admin);
            }
        }

        private Guid GetUserId()
        {
            var userIdClaim = _user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new InvalidOperationException($"Could not get user ID from claim {ClaimTypes.NameIdentifier}.");
            }

            return userId;
        }

        private string GetClaimValue(string claimType)
        {
            return _user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
