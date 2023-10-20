using e = Meal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meal.Domain.Enums;
using Meal.Domain.Entities;

namespace Meal.Infrastructure.Configurations
{
    public class MealConfiguration : IEntityTypeConfiguration<e.Meal>
    {
        public void Configure(EntityTypeBuilder<e.Meal> builder)
        {
            builder.ToTable("MealTable", builder => builder.IsTemporal(true));

            builder.HasKey(x => x.Id);

            builder.HasMany(m => m.Foods)
               .WithMany(f => f.Meals);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasMaxLength(128)
                .HasColumnName("MealsUser")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.Property(f => f.MealType)
                .HasColumnType("int")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(f => f.TotalCalorie)
                .HasColumnName("Calorie")
                .HasColumnType("int")
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(f => f.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("datetime2")
                .HasColumnOrder(5)
                .IsRequired(true);

            builder.Property(f => f.UpdatedDate)
                .HasColumnType("datetime2")
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(f => f.DeletedDate)
                .HasColumnType("datetime2")
                .HasColumnOrder(7)
                .IsRequired(false);

            builder.Property(f => f.Status)
                .HasDefaultValue(Status.Added)
                .HasColumnType("int")
                .HasColumnOrder(8)
                .IsRequired(true);
        }
    }
}
