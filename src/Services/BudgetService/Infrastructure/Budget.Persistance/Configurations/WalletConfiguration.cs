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

            builder.HasKey(x => x.Id);

            builder.HasOne(f => f.BudgetAccount)
               .WithMany(m => m.Wallets);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(f => f.WalletName)
                .HasColumnName("WalletName")
                .HasColumnType("nvarchar(128)")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(f => f.Currency)
                .HasColumnName("Currency")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(f => f.Total)
                .HasColumnType("int")
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(f => f.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(5)
                .IsRequired(true);
        }
    }
}
