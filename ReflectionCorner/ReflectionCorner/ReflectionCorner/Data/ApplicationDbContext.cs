﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ParentReview)
                .WithMany()
                .HasForeignKey(r => r.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}