using Microsoft.EntityFrameworkCore;
using Project_GeoService.Models;

namespace Project_GeoService.Data
{
    public class Project_GeoServiceContext : DbContext
    {
        public Project_GeoServiceContext (DbContextOptions<Project_GeoServiceContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<Continent> Continent { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<River> River { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<Continent>()
                .ToTable("Continent");

            modelBuilder.Entity<Country>()
                .ToTable("Country")
                .HasOne(c => c.Continent)
                .WithMany(c => c.Countries)
                .HasForeignKey(c => c.ContinentId);

            modelBuilder.Entity<River>()
                .ToTable("River");
        }
    }
}
