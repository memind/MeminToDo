﻿using Entertainment.Domain.Entities;
using Entertainment.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entertainment.Application.DTOs.BookDTOs
{
    public class BookDto
    {
        public BookDto()
        {
            Genres = new List<BookGenre>();
            BookNotes = new List<BookNote>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int PageCount { get; set; }
        public bool IsFinished { get; set; } = false;
        public List<BookGenre> Genres { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<BookNote>? BookNotes { get; set; }
    }
}
