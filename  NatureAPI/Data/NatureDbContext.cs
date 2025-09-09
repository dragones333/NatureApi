using NatureAPI.Models;

namespace NatureAPI.Data;

using Microsoft.EntityFrameworkCore;
using System;

public class NatureDbContext : DbContext
{
    public NatureDbContext(DbContextOptions<NatureDbContext> options) : base(options) {}

    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Trail> Trails { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Amenity> Amenities { get; set; } = null!;
    public DbSet<PlaceAmenity> PlaceAmenities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PlaceAmenity>()
            .HasKey(pa => new { pa.PlaceId, pa.AmenityId });
        
        modelBuilder.Entity<Place>()
            .HasMany(p => p.Trails)
            .WithOne(t => t.Place)
            .HasForeignKey(t => t.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Place>()
            .HasMany(p => p.Photos)
            .WithOne(ph => ph.Place)
            .HasForeignKey(ph => ph.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Place>()
            .HasMany(p => p.Reviews)
            .WithOne(r => r.Place)
            .HasForeignKey(r => r.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PlaceAmenity>()
            .HasOne(pa => pa.Place)
            .WithMany(p => p.PlaceAmenities)
            .HasForeignKey(pa => pa.PlaceId);

        modelBuilder.Entity<PlaceAmenity>()
            .HasOne(pa => pa.Amenity)
            .WithMany(a => a.PlaceAmenities)
            .HasForeignKey(pa => pa.AmenityId);
        
        modelBuilder.Entity<Amenity>().HasData(
            new Amenity { Id = 1, Name = "Baños" },
            new Amenity { Id = 2, Name = "Estacionamiento" },
            new Amenity { Id = 3, Name = "Mirador" },
            new Amenity { Id = 4, Name = "Área de picnic" }
        );
        
        modelBuilder.Entity<Place>().HasData(
            new Place {
                Id = 1,
                Name = "Hierve el Agua",
                Description = "Formaciones rocosas y cascadas petrificadas en Oaxaca.",
                Category = "Mirador",
                Latitude = 16.9000,
                Longitude = -96.4731,
                ElevationMeters = 1600,
                Accessible = true,
                EntryFee = 50.0,
                OpeningHours = "08:00-18:00",
                CreatedAt = DateTime.UtcNow
            },
            new Place {
                Id = 2,
                Name = "Cascada de Tamul",
                Description = "Impresionante cascada en San Luis Potosí, acceso por sendero o lancha.",
                Category = "Cascada",
                Latitude = 21.3226,
                Longitude = -99.1319,
                ElevationMeters = 200,
                Accessible = false,
                EntryFee = 0.0,
                OpeningHours = "06:00-17:00",
                CreatedAt = DateTime.UtcNow
            },
            new Place {
                Id = 3,
                Name = "Parque Nacional Iztaccíhuatl-Popocatépetl",
                Description = "Gran parque nacional con miradores y senderos.",
                Category = "Parque",
                Latitude = 19.1200,
                Longitude = -98.6270,
                ElevationMeters = 3500,
                Accessible = true,
                EntryFee = 0.0,
                OpeningHours = "00:00-23:59",
                CreatedAt = DateTime.UtcNow
            }
        );
        
        modelBuilder.Entity<Trail>().HasData(
            new Trail {
                Id = 1, PlaceId = 1, Name = "Sendero Mirador", DistanceKm = 1.2, EstimatedTimeMinutes = 40, Difficulty = "Fácil", Path = "[(16.9,-96.4731),(16.901,-96.474)]", IsLoop = false
            },
            new Trail {
                Id = 2, PlaceId = 2, Name = "Ruta a Tamul (río + caminata)", DistanceKm = 6.5, EstimatedTimeMinutes = 180, Difficulty = "Moderado", Path = "[(21.3226,-99.1319),(21.33,-99.14)]", IsLoop = false
            },
            new Trail {
                Id = 3, PlaceId = 3, Name = "Ascenso a mirador", DistanceKm = 3.4, EstimatedTimeMinutes = 120, Difficulty = "Difícil", Path = "[(19.12,-98.627),(19.13,-98.63)]", IsLoop = false
            }
        );
        
        modelBuilder.Entity<Photo>().HasData(
            new Photo { Id = 1, PlaceId = 1, Url = "https://upload.wikimedia.org/wikipedia/commons/1/12/Hierve_el_Agua_01.jpg", Description = "Vista de las cascadas petrificadas" },
            new Photo { Id = 2, PlaceId = 2, Url = "https://upload.wikimedia.org/wikipedia/commons/4/4b/Cascada_Tamul.jpg", Description = "Cascada de Tamul" },
            new Photo { Id = 3, PlaceId = 3, Url = "https://upload.wikimedia.org/wikipedia/commons/7/74/Iztaccihuatl.jpg", Description = "Iztaccíhuatl desde el parque" }
        );
        
        modelBuilder.Entity<PlaceAmenity>().HasData(
            new PlaceAmenity { PlaceId = 1, AmenityId = 3 }, 
            new PlaceAmenity { PlaceId = 1, AmenityId = 2 }, 
            new PlaceAmenity { PlaceId = 2, AmenityId = 2 }, 
            new PlaceAmenity { PlaceId = 3, AmenityId = 1 }, 
            new PlaceAmenity { PlaceId = 3, AmenityId = 4 }  
        );
    }
}
