using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookService : IBookService
    {
        private readonly IBookReadRepository _read;
        private readonly IBookWriteRepository _write;
        private readonly IBookNoteReadRepository _bookNote;
        private readonly IMapper _mapper;

        public BookService(IBookWriteRepository bookWriteRepository, IBookReadRepository bookReadRepository, IMapper mapper, IBookNoteReadRepository bookNote)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _bookNote = bookNote;
        }

        public int CreateBook(BookDto entity)
        {
            return _write.Create(_mapper.Map<Book>(entity));
        }

        public async Task<int> CreateBookAsync(BookDto entity)
        {
            return await _write.CreateAsync(_mapper.Map<Book>(entity));
        }

        public int DeleteBook(string id)
        {
            return _write.Delete(id);
        }

        public async Task<int> DeleteBookAsync(string id)
        {
            return await _write.DeleteAsync(id);
        }

        public List<BookDto> GetAllBooks()
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
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
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
            return _mapper.Map<List<BookDto>>(books);
        }

        public BookDto GetBookById(string id)
        {
            var book = _read.GetById(id);
            var notes = _bookNote.GetUsersAll(book.UserId.ToString());

            foreach (var note in notes)
            {
                if (book.Id == note.BookId)
                    book.BookNotes.Add(note);
            }

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBookByIdAsync(string id)
        {
            var book = await _read.GetByIdAsync(id);
            var notes = await _bookNote.GetUsersAllAsync(book.UserId.ToString());

            foreach (var note in notes)
            {
                if (book.Id == note.BookId)
                    book.BookNotes.Add(note);
            }

            return _mapper.Map<BookDto>(book);
        }

        public List<BookDto> GetUsersAllBooks(string userId)
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
            
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<List<BookDto>> GetUsersAllBooksAsync(string userId)
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

            return _mapper.Map<List<BookDto>>(books);
        }

        public int UpdateBook(BookDto entity)
        {
            return _write.Update(_mapper.Map<Book>(entity));
        }

        public async Task<int> UpdateBookAsync(BookDto entity)
        {
            return await _write.UpdateAsync(_mapper.Map<Book>(entity));
        }
    }
}
