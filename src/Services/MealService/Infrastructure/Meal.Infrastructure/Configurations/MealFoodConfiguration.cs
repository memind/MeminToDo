using Meal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meal.Infrastructure.Configurations
{
    public class MealFoodConfiguration : IEntityTypeConfiguration<MealFood>
    {
        public void Configure(EntityTypeBuilder<MealFood> builder)
        {
            builder.ToTable("MealFoodCrossTable");

            builder.HasKey(mf => new { mf.MealId, mf.FoodId});

            builder.Property(x => x.MealId)
                .HasColumnName("Meal")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(1)
                .IsRequired(true);

            builder.Property(x => x.FoodId)
                .HasColumnName("Food")
                .HasColumnType("uniqueidentifier")
                .HasColumnOrder(2)
                .IsRequired(true);

            builder.HasOne(mf => mf.Meal)
                .WithMany(m => m.Foods)
                .HasForeignKey(mf => mf.MealId)
                .HasConstraintName("FoodsOfMeal");

            builder.HasOne(mf => mf.Food)
                .WithMany(f => f.Meals)
                .HasForeignKey(mf => mf.FoodId)
                .HasConstraintName("MealsOfFood");
        }
    }
}
