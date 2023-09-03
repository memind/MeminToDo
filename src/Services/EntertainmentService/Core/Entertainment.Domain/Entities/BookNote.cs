using Entertainment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entertainment.Domain.Entities
{
    public class BookNote : EntertainmentBase
    {
        public string NoteHeader { get; set; }
        public string NoteContent { get; set; }
        public Guid BookId { get; set; }
    }
}
