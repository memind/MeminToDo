using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookNoteController : ControllerBase
    {
        private readonly IBookNoteService _service;

        public BookNoteController(IBookNoteService service)
        {
            _service = service;
        }

        [HttpGet("/getBookNote")]
        [Authorize(Policy = "EntertainmentRead")]
        public BookNoteDto GetBookNote(string id) => _service.GetBookNoteById(id);

        [HttpGet("/getBookNotes")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<BookNoteDto> GetAllBookNotes() => _service.GetAllBookNotes();

        [HttpGet("/getUsersBookNotes")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<BookNoteDto> GetUsersAllBookNotes(string id) => _service.GetUsersAllBookNotes(id);

        [HttpPost("/createBookNote")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int CreateBookNote(BookNoteDto model) => _service.CreateBookNote(model);

        [HttpPut("/updateBookNote")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int UpdateBookNote(BookNoteDto model) => _service.UpdateBookNote(model);

        [HttpDelete("/deleteBookNote")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int DeleteBookNote(string id) => _service.DeleteBookNote(id);
    }
}
