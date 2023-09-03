using System.ComponentModel.DataAnnotations;

namespace Entertainment.Domain.Entities.Common
{
    public class EntertainmentBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
