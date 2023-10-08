using e = Meal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Meal.Domain.Common;
using System.Reflection;
using Meal.Infrastructure.Configurations;

namespace Meal.Infrastructure.Context
{
    public class MealDbContext : DbContext
    {
        public MealDbContext(DbContextOptions options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<e.Food> Foods { get; set; }
        public DbSet<e.Meal> Meals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FoodConfiguration());
            modelBuilder.ApplyConfiguration(new MealConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            bool acceptStatus = false;
            int changes = 0;

            try
            {
                var datas = ChangeTracker.Entries<BaseEntity>();

                foreach (var data in datas)
                {

                    if (data.State == EntityState.Added)
                        data.Entity.CreatedDate = DateTime.Now;

                    if (data.State == EntityState.Modified)
                    {
                        if(data.Entity.Status == Domain.Enums.Status.Deleted)
                        {
                            data.Entity.DeletedDate = DateTime.Now;
                            continue;
                        }
                        
                        data.Entity.UpdatedDate = DateTime.Now;
                        data.Entity.Status = Domain.Enums.Status.Modified;
                    }

                    if (data.State == EntityState.Deleted)
                    {
                        data.Entity.DeletedDate = DateTime.Now;
                        data.Entity.Status = Domain.Enums.Status.Deleted;
                    }
                }

                changes = base.SaveChanges(false);
                acceptStatus = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

            if (acceptStatus)
                base.ChangeTracker.AcceptAllChanges();

            return changes;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            bool acceptStatus = false;
            int changes = 0;

            try
            {
                var datas = ChangeTracker.Entries<BaseEntity>();

                foreach (var data in datas)
                {
                    if (data.State == EntityState.Added)
                        data.Entity.CreatedDate = DateTime.Now;

                    if (data.State == EntityState.Modified)
                    {
                        data.Entity.UpdatedDate = DateTime.Now;
                        data.Entity.Status = Domain.Enums.Status.Modified;
                    }

                    if (data.State == EntityState.Deleted)
                    {
                        data.Entity.DeletedDate = DateTime.Now;
                        data.Entity.Status = Domain.Enums.Status.Deleted;
                    }
                }

                changes = await base.SaveChangesAsync(false, cancellationToken);
                acceptStatus = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

            if (acceptStatus)
                base.ChangeTracker.AcceptAllChanges();

            return changes;
        }
    }
}
