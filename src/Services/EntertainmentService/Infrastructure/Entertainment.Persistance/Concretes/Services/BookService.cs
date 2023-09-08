using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookService : IBookService
    {
        private readonly IBookReadRepository _read;
        private readonly IBookWriteRepository _write;
        private readonly IBookNoteReadRepository _bookNote;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookWriteRepository bookWriteRepository, IBookReadRepository bookReadRepository, IMapper mapper, IBookNoteReadRepository bookNote, ILogger<BookService> logger)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _bookNote = bookNote;
            _logger = logger;
        }

        public int CreateBook(BookDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Book: {entity.BookName} - {entity.UserId}");
                return _write.Create(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> CreateBookAsync(BookDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Book: {entity.BookName} - {entity.UserId}");
                return await _write.CreateAsync(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int DeleteBook(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Book: {id}");
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> DeleteBookAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Book: {id}");
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<BookDto> GetAllBooks()
        {
            try
            {
                var notes = _bookNote.GetAll();
                var books = _read.GetAll();

                foreach (var book in books)
                {
                    foreach (var note in notes)
                    {
                        if (note.BookId == book.Id && note.UserId == book.UserId)
                            book.BookNotes.Add(note);
                    }
                }

                _logger.LogInformation("Getting All Books");
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var notes = await _bookNote.GetAllAsync();
                var books = await _read.GetAllAsync();

                foreach (var book in books)
                {
                    foreach (var note in notes)
                    {
                        if (note.BookId == book.Id && note.UserId == book.UserId)
                            book.BookNotes.Add(note);
                    }
                }

                _logger.LogInformation("Getting All Books");
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public BookDto GetBookById(string id)
        {
            try
            {
                var book = _read.GetById(id);
                var notes = _bookNote.GetUsersAll(book.UserId.ToString());

                foreach (var note in notes)
                {
                    if (book.Id == note.BookId)
                        book.BookNotes.Add(note);
                }

                _logger.LogInformation($"Getting Book: {id}");
                return _mapper.Map<BookDto>(book);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<BookDto> GetBookByIdAsync(string id)
        {
            try
            {
                var book = await _read.GetByIdAsync(id);
                var notes = await _bookNote.GetUsersAllAsync(book.UserId.ToString());

                foreach (var note in notes)
                {
                    if (book.Id == note.BookId)
                        book.BookNotes.Add(note);
                }

                _logger.LogInformation($"Getting Book: {id}");
                return _mapper.Map<BookDto>(book);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<BookDto> GetUsersAllBooks(string userId)
        {
            try
            {
                var books = _read.GetUsersAll(userId);
                var notes = _bookNote.GetUsersAll(userId);

                foreach (var book in books)
                {
                    foreach (var note in notes)
                    {
                        if (book.Id == note.BookId)
                            book.BookNotes.Add(note);
                    }
                }

                _logger.LogInformation("Getting Users All Books");
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<BookDto>> GetUsersAllBooksAsync(string userId)
        {
            try
            {
                var books = await _read.GetUsersAllAsync(userId);
                var notes = await _bookNote.GetUsersAllAsync(userId);

                foreach (var book in books)
                {
                    foreach (var note in notes)
                    {
                        if (book.Id == note.BookId)
                            book.BookNotes.Add(note);
                    }
                }

                _logger.LogInformation("Getting Users All Books");
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int UpdateBook(BookDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Book Note: {entity.BookName}");
                return _write.Update(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> UpdateBookAsync(BookDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Book Note: {entity.BookName}");
                return await _write.UpdateAsync(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }
    }
}
