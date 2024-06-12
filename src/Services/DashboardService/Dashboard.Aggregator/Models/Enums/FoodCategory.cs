using System.ComponentModel.DataAnnotations;

namespace Dashboard.Aggregator.Models.Enums
{
    public enum FoodCategory
    {

        [Display(Name = "Vegetables")]
        Vegetables = 1,

        [Display(Name = "Fruits")]
        Fruits = 2,

        [Display(Name = "Grains")]
        Grains = 3,

        [Display(Name = "Milk Products")]
        MilkProducts = 4,

        [Display(Name = "Eggs")]
        Eggs = 5,

        [Display(Name = "Meats")]
        Meats = 6,

        [Display(Name = "Fishes")]
        Fishes = 7,

        [Display(Name = "Fats/Oils")]
        Fats_Oils = 9,

        [Display(Name = "Legumes Nuts Seeds")]
        Legumes_Nuts_Seeds = 9,

        [Display(Name = "Sugars")]
        Sugars = 10,

        [Display(Name = "Non Alcoholic Beverages")]
        Non_Alcoholic_Beverages = 11,

        [Display(Name = "Alcoholic Beverages")]
        Alcoholic_Beverages = 12
    }
}
