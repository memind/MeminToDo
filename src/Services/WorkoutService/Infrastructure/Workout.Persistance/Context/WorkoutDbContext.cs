using Microsoft.EntityFrameworkCore;
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
            optionsBuilder.UseCosmos(
                            "https://memind2112.documents.azure.com:443/",
                            "HcK7NUP8b0Unn8jyVKcgbaPpsw7sEP12GQ1kv4T4mNvetyVUzSUAuTfBf3V2jsn9FVlL5R2rbrgAACDbmqgQYQ==",
                            databaseName: "MeminToDo.WorkoutDB");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<p.Exercise>().ToContainer("Exercises").HasPartitionKey(e => e.Id);
            modelBuilder.Entity<p.Workout>().ToContainer("Workouts").HasPartitionKey(w => w.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
