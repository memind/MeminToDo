using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Net;
using p = Workout.Domain.Entities;

namespace Workout.Persistance.Context
{
    public class WorkoutDbContext : DbContext
    {
        public WorkoutDbContext(DbContextOptions options) : base(options) { }
        public DbSet<p.Workout> Workouts { get; set; }
        public DbSet<p.Exercise> Exercises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<p.Exercise>().ToContainer("Exercises").HasPartitionKey(e => e.Id);
            modelBuilder.Entity<p.Workout>().ToContainer("Workouts").HasPartitionKey(w => w.Id);

            modelBuilder.Entity<p.Workout>().HasMany<p.Exercise>(w => w.Exercises);
            modelBuilder.Entity<p.Exercise>().HasOne<p.Workout>(e => e.Workout).WithMany(w => w.Exercises).HasForeignKey(e => e.WorkoutId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
