using Microsoft.EntityFrameworkCore;
using ReflectionCorner.Models;

namespace ReflectionCorner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<CustomReviewType> CustomReviewTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Add indexes for search performance
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.Title);

            modelBuilder.Entity<Review>()
                .HasIndex(r => r.Type);

            modelBuilder.Entity<Review>()
                .HasIndex(r => r.IsPublic);

            // Configure CustomReviewType entity
            modelBuilder.Entity<CustomReviewType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Review entity relationships
            modelBuilder.Entity<Review>()
                .HasOne(r => r.CustomReviewType)
                .WithMany()
                .HasForeignKey(r => r.CustomReviewTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TVShow>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Episode>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Episode>()
                .HasOne(e => e.TVShow)
                .WithMany(t => t.Episodes)
                .HasForeignKey(e => e.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}