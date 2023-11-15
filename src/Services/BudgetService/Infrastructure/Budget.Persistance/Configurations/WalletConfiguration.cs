using Budget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Persistance.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("WalletTable");

            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.BudgetAccount)
               .WithMany(ba => ba.Wallets);

            builder.Property(w => w.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(w => w.WalletName)
                .HasColumnName("WalletName")
                .HasColumnType("nvarchar(128)")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(w => w.Currency)
                .HasColumnName("Currency")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(w => w.Total)
                .HasColumnType("int")
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(w => w.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(5)
                .IsRequired(true);
        }
    }
}
