using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;

namespace MovieCatalog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieCountry> MovieCountries { get; set; }
        public DbSet<MovieProducer> MovieProducers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new {ma.MovieId, ma.ActorId});
            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.MovieActors)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(ma => ma.MovieActors)
                .HasForeignKey(a => a.ActorId);

            modelBuilder.Entity<MovieCategory>()
                .HasKey(mc => new {mc.MovieId, mc.CategoryId});
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieCategory>()
                .HasOne(c => c.Category)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<MovieProducer>()
                .HasKey(mp => new { mp.MovieId, mp.ProducerId });
            modelBuilder.Entity<MovieProducer>()
                .HasOne(m => m.Movie)
                .WithMany(mp => mp.MovieProducers)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieProducer>()
                .HasOne(p => p.Producer)
                .WithMany(mp => mp.MovieProducers)
                .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<MovieCountry>()
                .HasKey(mc => new {mc.MovieId, mc.CountryId});
            modelBuilder.Entity<MovieCountry>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCountries)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieCountry>()
                .HasOne(c => c.Country)
                .WithMany(mc => mc.MovieCountries)
                .HasForeignKey(c => c.CountryId);
        }
    }
}
