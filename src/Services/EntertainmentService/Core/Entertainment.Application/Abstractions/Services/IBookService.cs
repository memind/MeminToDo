using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Abstractions.Services
{
    public interface IBookService
    {
        int CreateBook(BookDto entity);
        Task<int> CreateBookAsync(BookDto entity);

        int DeleteBook(string id);
        Task<int> DeleteBookAsync(string id);

        int UpdateBook(BookDto entity);
        Task<int> UpdateBookAsync(BookDto entity);

        public List<BookDto> GetAllBooks();
        Task<List<BookDto>> GetAllBooksAsync();

        BookDto GetBookById(string id);
        Task<BookDto> GetBookByIdAsync(string id);

        List<BookDto> GetUsersAllBooks(string userId);
        Task<List<BookDto>> GetUsersAllBooksAsync(string userId);

        public void ConsumeBackUpInfo();
        public void ConsumeTestInfo();
    }
}
