using System.ComponentModel.DataAnnotations;

namespace Meal.Domain.Enums
{
    public enum MealType
    {

        [Display(Name = "Breakfast")]
        Breakfast = 1,

        [Display(Name = "Lunch")]
        Lunch = 2,

        [Display(Name = "Dinner")]
        Dinner = 3,

        [Display(Name = "Snack")]
        Snack = 4,
    }
}
