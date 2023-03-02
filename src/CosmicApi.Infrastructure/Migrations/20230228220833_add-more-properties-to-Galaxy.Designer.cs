﻿// <auto-generated />
using System;
using CosmicApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CosmicApi.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230228220833_add-more-properties-to-Galaxy")]
    partial class addmorepropertiestoGalaxy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CosmicApi.Domain.Entities.BlackHole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GalaxyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GalaxyId");

                    b.ToTable("BlackHole");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Galaxy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AbsoluteMagnitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EscapeVelocity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("NumberOfStars")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Radius")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Galaxies");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Moon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<Guid>("PlanetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.ToTable("Moons");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<Guid>("LuminaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GalaxyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("NumberOfMoons")
                        .HasColumnType("int");

                    b.Property<Guid>("StarId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GalaxyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("StarId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("UserId1")
                        .IsUnique()
                        .HasFilter("[UserId1] IS NOT NULL");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Star", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GalaxyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("NumberOfPlanets")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GalaxyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.BlackHole", b =>
                {
                    b.HasOne("CosmicApi.Domain.Entities.Galaxy", "Galaxy")
                        .WithMany("BlackHoles")
                        .HasForeignKey("GalaxyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Galaxy");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Moon", b =>
                {
                    b.HasOne("CosmicApi.Domain.Entities.Planet", "Planet")
                        .WithMany("Moons")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Planet", b =>
                {
                    b.HasOne("CosmicApi.Domain.Entities.Galaxy", "Galaxy")
                        .WithMany("Planets")
                        .HasForeignKey("GalaxyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CosmicApi.Domain.Entities.Star", "Star")
                        .WithMany("Planets")
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Galaxy");

                    b.Navigation("Star");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("CosmicApi.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("CosmicApi.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CosmicApi.Domain.Entities.User", null)
                        .WithOne("RefreshToken")
                        .HasForeignKey("CosmicApi.Domain.Entities.RefreshToken", "UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Star", b =>
                {
                    b.HasOne("CosmicApi.Domain.Entities.Galaxy", "Galaxy")
                        .WithMany("Stars")
                        .HasForeignKey("GalaxyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Galaxy");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Galaxy", b =>
                {
                    b.Navigation("BlackHoles");

                    b.Navigation("Planets");

                    b.Navigation("Stars");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Planet", b =>
                {
                    b.Navigation("Moons");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.Star", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("CosmicApi.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}