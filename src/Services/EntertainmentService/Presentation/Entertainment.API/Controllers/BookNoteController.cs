using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookNoteController : ControllerBase
    {
        private readonly IBookNoteService _service;

        public BookNoteController(IBookNoteService service)
        {
            _service = service;
        }

        [HttpGet("/getBookNote")]
        public BookNoteDto GetBookNote(string id)
        {
            return _service.GetBookNoteById(id);
        }

        [HttpGet("/getBookNotes")]
        public List<BookNoteDto> GetAllBookNotes()
        {
            return _service.GetAllBookNotes();
        }

        [HttpGet("/getUsersBookNotes")]
        public List<BookNoteDto> GetUsersAllBookNotes(string id)
        {
            return _service.GetUsersAllBookNotes(id);
        }

        [HttpPost("/createBookNote")]
        public int CreateBookNote(BookNoteDto model)
        {
            return _service.CreateBookNote(model);
        }

        [HttpPut("/updateBookNote")]
        public int UpdateBookNote(BookNoteDto model)
        {
            return _service.UpdateBookNote(model);
        }

        [HttpDelete("/deleteBookNote")]
        public int DeleteBookNote(string id)
        {
            return _service.DeleteBookNote(id);
        }
    }
}
