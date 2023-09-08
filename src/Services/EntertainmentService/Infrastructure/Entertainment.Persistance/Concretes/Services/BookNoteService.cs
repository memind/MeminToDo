using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookNoteService : IBookNoteService
    {
        private readonly IBookNoteReadRepository _read;
        private readonly IBookNoteWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<BookNoteService> _logger;

        public BookNoteService(IBookNoteWriteRepository bookWriteRepository, IBookNoteReadRepository bookReadRepository, IMapper mapper, ILogger<BookNoteService> logger)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateBookNote(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Book Note: {entity.NoteHeader} - {entity.UserId}");
                return _write.Create(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> CreateBookNoteAsync(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Book Note: {entity.NoteHeader} - {entity.UserId}");
                return await _write.CreateAsync(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int DeleteBookNote(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Book Note: {id}");
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> DeleteBookNoteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Book Note: {id}");
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<BookNoteDto> GetAllBookNotes()
        {
            try
            {
                var games = _read.GetAll();

                _logger.LogInformation("Getting All Book Notes");

                return _mapper.Map<List<BookNoteDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<BookNoteDto>> GetAllBookNotesAsync()
        {
            try
            {
                var games = await _read.GetAllAsync();

                _logger.LogInformation("Getting All Book Notes");

                return _mapper.Map<List<BookNoteDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public BookNoteDto GetBookNoteById(string id)
        {
            try
            {
                var game = _read.GetById(id);

                _logger.LogInformation($"Getting Book Note: {id}");

                return _mapper.Map<BookNoteDto>(game);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<BookNoteDto> GetBookNoteByIdAsync(string id)
        {
            try
            {
                var game = await _read.GetByIdAsync(id);

                _logger.LogInformation($"Getting Book Note: {id}");

                return _mapper.Map<BookNoteDto>(game);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<BookNoteDto> GetUsersAllBookNotes(string userId)
        {
            try
            {
                var games = _read.GetUsersAll(userId);

                _logger.LogInformation("Getting Users All Book Notes");

                return _mapper.Map<List<BookNoteDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<BookNoteDto>> GetUsersAllBookNotesAsync(string userId)
        {
            try
            {
                var games = await _read.GetUsersAllAsync(userId);

                _logger.LogInformation("Getting Users All Book Notes");

                return _mapper.Map<List<BookNoteDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int UpdateBookNote(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Book Note: {entity.NoteHeader}");
                return _write.Update(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> UpdateBookNoteAsync(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Book Note: {entity.NoteHeader}");
                return await _write.UpdateAsync(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }
    }
}
