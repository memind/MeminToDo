using Meal.Domain.Entities;
using Meal.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meal.Infrastructure.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("FoodTable");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasMaxLength(128)
                .HasColumnName("User")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(f => f.Name)
                .HasColumnName("FoodName")
                .HasColumnType("nvarchar(128)")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(f => f.Category)
                .HasColumnType("int")
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(f => f.CalorieByServing)
                .HasColumnName("Calorie")
                .HasColumnType("int")
                .HasColumnOrder(5)
                .IsRequired(true);

            builder.Property(f => f.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(6)
                .IsRequired(true);

            builder.Property(f => f.UpdatedDate)
                .HasColumnType("datetime2")
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(f => f.DeletedDate)
                .HasColumnType("datetime2")
                .HasColumnOrder(8)
                .IsRequired(false);

            builder.Property(f => f.Status)
                .HasDefaultValue(Status.Added)
                .HasColumnType("int")
                .HasColumnOrder(9)
                .IsRequired(true);
        }
    }
}
