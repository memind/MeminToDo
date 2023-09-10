using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dashboard.Aggregator.Models.EntertainmentModels
{
    public class BookNoteModel
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string NoteHeader { get; set; }
        public string NoteContent { get; set; }
        public Guid BookId { get; set; }

        public BookModel? Book { get; set; }
    }
}
