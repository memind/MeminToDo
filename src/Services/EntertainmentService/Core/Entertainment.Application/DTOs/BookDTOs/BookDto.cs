using Entertainment.Application.DTOs.BookNoteDTOs;
using Entertainment.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entertainment.Application.DTOs.BookDTOs
{
    public class BookDto
    {
        public BookDto()
        {
            BookNotes = new List<BookNoteDto>();
        }

        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public bool IsFinished { get; set; } = false;

        [NotMapped]
        public List<BookNoteDto>? BookNotes { get; set; }
    }
}
