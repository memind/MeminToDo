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

            builder.HasKey(ba => ba.Id);

            builder.HasMany(ba => ba.MoneyFlows)
               .WithOne(mf => mf.BudgetAccount);

            builder.HasMany(ba => ba.Wallets)
               .WithOne(w => w.BudgetAccount);

            builder.Property(ba => ba.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(ba => ba.UserId)
                .HasMaxLength(128)
                .HasColumnName("AccountsUserId")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(ba => ba.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(3)
                .IsRequired(true);
        }
    }
}
