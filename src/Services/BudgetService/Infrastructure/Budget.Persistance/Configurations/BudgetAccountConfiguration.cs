using Budget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Persistance.Configurations
{
    public class BudgetAccountConfiguration : IEntityTypeConfiguration<BudgetAccount>
    {
        public void Configure(EntityTypeBuilder<BudgetAccount> builder)
        {
            builder.ToTable("BudgetAccountTable");

            builder.HasKey(x => x.Id);

            builder.HasMany(f => f.MoneyFlows)
               .WithOne(m => m.BudgetAccount);

            builder.HasMany(f => f.Wallets)
               .WithOne(m => m.BudgetAccount);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasMaxLength(128)
                .HasColumnName("AccountsUserId")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(f => f.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(3)
                .IsRequired(true);
        }
    }
}
