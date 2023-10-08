using System.ComponentModel.DataAnnotations;

namespace Meal.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Added")]
        Added = 1,

        [Display(Name = "Modified")]
        Modified = 2,

        [Display(Name = "Deleted")]
        Deleted = 3
    }
}
