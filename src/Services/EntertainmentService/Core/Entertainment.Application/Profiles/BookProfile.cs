using AutoMapper;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();   
        }
    }
}