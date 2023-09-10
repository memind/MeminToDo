using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Aggregator.Models.EntertainmentModels
{
    public class BookModel
    {
        public BookModel()
        {
            BookNotes = new List<BookNoteModel>();
        }

        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public bool IsFinished { get; set; } = false;

        public List<BookNoteModel>? BookNotes { get; set; }
    }
}
