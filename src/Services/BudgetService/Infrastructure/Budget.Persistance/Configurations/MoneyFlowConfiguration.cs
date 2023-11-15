using Budget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Persistance.Configurations
{
    public class MoneyFlowConfiguration : IEntityTypeConfiguration<MoneyFlow>
    {
        public void Configure(EntityTypeBuilder<MoneyFlow> builder)
        {
            builder.ToTable("MoneyFlowTable");

            builder.HasKey(mf => mf.Id);

            builder.HasOne(mf => mf.BudgetAccount)
               .WithMany(ba => ba.MoneyFlows);

            builder.Property(mf => mf.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(mf => mf.UserId)
                .HasMaxLength(128)
                .HasColumnName("BudgetsUser")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(mf => mf.Currency)
                .HasColumnName("Currency")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(mf => mf.Type)
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(mf => mf.Amount)
                .HasColumnType("int")
                .HasColumnOrder(5)
                .IsRequired(true);

            builder.Property(mf => mf.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(128)")
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(mf => mf.Message)
                .HasColumnName("Message")
                .HasColumnType("nvarchar(12)")
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(mf => mf.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(8)
                .IsRequired(true);
        }
    }
}
