using AutoMapper;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Profiles
{
    public class BookNoteProfile : Profile
    {
        public BookNoteProfile()
        {
            CreateMap<BookNote, BookNoteDto>().ReverseMap();
        }
    }
}