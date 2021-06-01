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

        public DbSet<City> Cities { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryCapital> CountryCapitals { get; set; }
        public DbSet<CountryRiver> CountryRivers { get; set; }
        public DbSet<River> Rivers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId)
                .HasConstraintName("FK_City_Country");

            modelBuilder.Entity<Continent>()
                .ToTable("Continent")
                .Property(c => c.Population)
                .HasField("_population");

            modelBuilder.Entity<Country>()
                .ToTable("Country")
                .HasOne(c => c.Continent)
                .WithMany(c => c.Countries)
                .HasForeignKey(c => c.ContinentId)
                .HasConstraintName("FK_Country_Continent");

            modelBuilder.Entity<CountryCapital>(entity =>
            {
                entity.ToTable("CountryCapital");
                entity.HasKey(x => new { x.CountryId, x.CityId });

                entity.HasOne(cc => cc.Country)
                    .WithMany(c => c.CountryCapitals)
                    .HasForeignKey(cc => cc.CountryId)
                    .HasConstraintName("FK_CountryCapital_Country")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cc => cc.City)
                    .WithMany(c => c.CountryCapitals)
                    .HasForeignKey(cc => cc.CityId)
                    .HasConstraintName("FK_CountryCapital_City")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CountryRiver>(entity =>
            {
                entity.ToTable("CountryRiver");
                entity.HasKey(x => new { x.CountryId, x.RiverId });

                entity.HasOne(cr => cr.Country)
                    .WithMany(c => c.CountryRivers)
                    .HasForeignKey(cr => cr.CountryId)
                    .HasConstraintName("FK_CountryRiver_Country")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cr => cr.River)
                    .WithMany(r => r.CountryRivers)
                    .HasForeignKey(cr => cr.RiverId)
                    .HasConstraintName("FK_CountryRiver_River")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<River>()
                .ToTable("River");
        }
    }
}
