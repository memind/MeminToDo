using Entertainment.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entertainment.Application.DTOs.BookNoteDTOs
{
    public class BookNoteDto
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string NoteHeader { get; set; }
        public string NoteContent { get; set; }
        public Guid BookId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
