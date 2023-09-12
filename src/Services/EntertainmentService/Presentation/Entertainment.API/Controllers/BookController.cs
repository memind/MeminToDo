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

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("/getBook")]
        public BookDto GetBook(string id)
        {
            return _service.GetBookById(id);
        }

        [HttpGet("/getBooks")]
        public List<BookDto> GetAllBooks()
        {
            return _service.GetAllBooks();
        }

        [HttpGet("/getUsersBooks")]
        public List<BookDto> GetUsersAllBooks(string id)
        {
            return _service.GetUsersAllBooks(id);
        }

        [HttpPost("/createBook")]
        public int CreateBook(BookDto model)
        {
            return _service.CreateBook(model);
        }

        [HttpPut("/updateBook")]
        public int UpdateBook(BookDto model)
        {
            return _service.UpdateBook(model);
        }

        [HttpDelete("/deleteBook")]
        public int DeleteBook(string id)
        {
            return _service.DeleteBook(id);
        }
    }
}
