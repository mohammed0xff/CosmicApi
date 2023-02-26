using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Infrastructure.Context
{
    public interface IContext : IAsyncDisposable, IDisposable
    {
        public DatabaseFacade Database { get; }
        public DbSet<Galaxy> Galaxies { get; set; }
        public DbSet<Star> Stars { get; set; } 
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Moon> Moons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> Tokens { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}