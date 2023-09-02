using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookService : IBookService
    {
        private readonly IBookReadRepository _read;
        private readonly IBookWriteRepository _write;
        private readonly IMapper _mapper;

        public BookService(IBookWriteRepository bookWriteRepository, IBookReadRepository bookReadRepository, IMapper mapper)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
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
            var books = _read.GetAll();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _read.GetAllAsync();
            return _mapper.Map<List<BookDto>>(books);
        }

        public BookDto GetBookById(string id)
        {
            var book = _read.GetById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBookByIdAsync(string id)
        {
            var book = await _read.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public List<BookDto> GetUsersAllBooks(string userId)
        {
            var books = _read.GetUsersAll(userId);
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<List<BookDto>> GetUsersAllBooksAsync(string userId)
        {
            var books = await _read.GetUsersAllAsync(userId);
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
