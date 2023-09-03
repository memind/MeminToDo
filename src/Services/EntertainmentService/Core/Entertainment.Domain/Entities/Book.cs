using Entertainment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entertainment.Domain.Entities
{
    public class Book : EntertainmentBase
    {
        public Book()
        {
            BookNotes = new List<BookNote>();
        }

        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public bool IsFinished { get; set; } = false;

        [NotMapped]
        [JsonIgnore]
        public List<BookNote>? BookNotes { get; set; }
    }
}
