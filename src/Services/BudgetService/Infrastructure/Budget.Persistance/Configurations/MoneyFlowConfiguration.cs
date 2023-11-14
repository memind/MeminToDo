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

            builder.HasKey(x => x.Id);

            builder.HasOne(f => f.BudgetAccount)
               .WithMany(m => m.MoneyFlows);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasMaxLength(128)
                .HasColumnName("BudgetsUser")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(f => f.Currency)
                .HasColumnName("Currency")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(f => f.Type)
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(f => f.Amount)
                .HasColumnType("int")
                .HasColumnOrder(5)
                .IsRequired(true);

            builder.Property(f => f.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(128)")
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(f => f.Message)
                .HasColumnName("Message")
                .HasColumnType("nvarchar(12)")
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(f => f.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(8)
                .IsRequired(true);
        }
    }
}
