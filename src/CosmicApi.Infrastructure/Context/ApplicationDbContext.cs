﻿using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Configuration;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace CosmicApi.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Galaxy> Galaxies { get; set; } = null!;
    public DbSet<Star> Stars { get; set; } = null!;
    public DbSet<Planet> Planets { get; set; } = null!;
    public DbSet<Moon> Moons { get; set; } = null!;
    public DbSet<Picture> Pictures { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> Tokens { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
