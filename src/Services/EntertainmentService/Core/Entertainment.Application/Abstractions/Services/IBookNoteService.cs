
using Entertainment.Application.DTOs.BookNoteDTOs;

namespace Entertainment.Application.Abstractions.Services
{
    public interface IBookNoteService
    {
        int CreateBookNote(BookNoteDto entity);
        Task<int> CreateBookNoteAsync(BookNoteDto entity);

        int DeleteBookNote(string id);
        Task<int> DeleteBookNoteAsync(string id);

        int UpdateBookNote(BookNoteDto entity);
        Task<int> UpdateBookNoteAsync(BookNoteDto entity);

        public List<BookNoteDto> GetAllBookNotes();
        Task<List<BookNoteDto>> GetAllBookNotesAsync();

        BookNoteDto GetBookNoteById(string id);
        Task<BookNoteDto> GetBookNoteByIdAsync(string id);

        List<BookNoteDto> GetUsersAllBookNotes(string userId);
        Task<List<BookNoteDto>> GetUsersAllBookNotesAsync(string userId);
    }
}
