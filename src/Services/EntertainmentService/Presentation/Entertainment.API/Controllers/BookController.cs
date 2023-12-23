using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service) => _service = service;
        

        [HttpGet("/{id}")]
        [Authorize(Policy = "EntertainmentRead")]
        public BookDto GetBook(string id) => _service.GetBookById(id);
        

        [HttpGet]
        [Authorize(Policy = "EntertainmentRead")]
        public List<BookDto> GetAllBooks() => _service.GetAllBooks();

        [HttpGet("/user/{id}")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<BookDto> GetUsersAllBooks(string id) => _service.GetUsersAllBooks(id);
        

        [HttpPost]
        [Authorize(Policy = "EntertainmentWrite")]
        public int CreateBook(BookDto model) => _service.CreateBook(model);
        

        [HttpPut]
        [Authorize(Policy = "EntertainmentWrite")]
        public int UpdateBook(BookDto model) => _service.UpdateBook(model);
        

        [HttpDelete("/{id}")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int DeleteBook(string id) => _service.DeleteBook(id);
    }
}
