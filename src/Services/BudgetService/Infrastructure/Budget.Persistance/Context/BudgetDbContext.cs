using Budget.Domain.Entities;
using Budget.Domain.Entities.Common;
using Budget.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Budget.Persistance.Context
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<MoneyFlow> MoneyFlows { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<BudgetAccount> BudgetAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BudgetAccountConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new MoneyFlowConfiguration());

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
                    if (data.State == EntityState.Added)
                        data.Entity.CreatedDate = DateTime.Now;

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
                    if (data.State == EntityState.Added)
                        data.Entity.CreatedDate = DateTime.Now;

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