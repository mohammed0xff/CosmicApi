using CosmicApi.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CosmicApi.Application.Common.Session
{
    public class Session : ISession
    {
        public DateTime Now => DateTime.Now;
        public Guid UserId { get; } 
        public string Username { get; } = null!;
        public string Email { get; set; }
        public bool IsAuthenticated { get; } = false;
        public bool IsAdmin { get; } = false;

        public Session(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            IsAuthenticated = user.Identity.IsAuthenticated;
            if (IsAuthenticated)
            {
                UserId = Guid.Parse(user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
                Email = user.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
                Username = user.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                IsAdmin = user.IsInRole(Roles.Admin);
            }
        }
    }
}
