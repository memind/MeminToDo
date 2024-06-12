using System.ComponentModel.DataAnnotations;

namespace Dashboard.Aggregator.Models.Enums
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
