using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Infrastructure.Context
{
    public interface IContext : IAsyncDisposable, IDisposable
    {
        public DatabaseFacade Database { get; }
        public DbSet<Galaxy> Galaxies { get; }
        public DbSet<Star> Stars { get; } 
        public DbSet<Planet> Planets { get; }
        public DbSet<Picture> Pictures { get; }
        public DbSet<Moon> Moons { get; }
        public DbSet<User> Users { get; }
        public DbSet<RefreshToken> Tokens { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}