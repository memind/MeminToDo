using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookNoteService : IBookNoteService
    {
        private readonly IBookNoteReadRepository _read;
        private readonly IBookNoteWriteRepository _write;
        private readonly IMapper _mapper;

        public BookNoteService(IBookNoteWriteRepository bookWriteRepository, IBookNoteReadRepository bookReadRepository, IMapper mapper)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
        }

        public int CreateBookNote(BookNoteDto entity)
        {
            return _write.Create(_mapper.Map<BookNote>(entity));
        }

        public async Task<int> CreateBookNoteAsync(BookNoteDto entity)
        {
            return await _write.CreateAsync(_mapper.Map<BookNote>(entity));
        }

        public int DeleteBookNote(string id)
        {
            return _write.Delete(id);
        }

        public async Task<int> DeleteBookNoteAsync(string id)
        {
            return await _write.DeleteAsync(id);
        }

        public List<BookNoteDto> GetAllBookNotes()
        {
            var games = _read.GetAll();
            return _mapper.Map<List<BookNoteDto>>(games);
        }

        public async Task<List<BookNoteDto>> GetAllBookNotesAsync()
        {
            var games = await _read.GetAllAsync();
            return _mapper.Map<List<BookNoteDto>>(games);
        }

        public BookNoteDto GetBookNoteById(string id)
        {
            var game = _read.GetById(id);
            return _mapper.Map<BookNoteDto>(game);
        }

        public async Task<BookNoteDto> GetBookNoteByIdAsync(string id)
        {
            var game = await _read.GetByIdAsync(id);
            return _mapper.Map<BookNoteDto>(game);
        }

        public List<BookNoteDto> GetUsersAllBookNotes(string userId)
        {
            var games = _read.GetUsersAll(userId);
            return _mapper.Map<List<BookNoteDto>>(games);
        }

        public async Task<List<BookNoteDto>> GetUsersAllBookNotesAsync(string userId)
        {
            var games = await _read.GetUsersAllAsync(userId);
            return _mapper.Map<List<BookNoteDto>>(games);
        }

        public int UpdateBookNote(BookNoteDto entity)
        {
            return _write.Update(_mapper.Map<BookNote>(entity));
        }

        public async Task<int> UpdateBookNoteAsync(BookNoteDto entity)
        {
            return await _write.UpdateAsync(_mapper.Map<BookNote>(entity));
        }
    }
}
