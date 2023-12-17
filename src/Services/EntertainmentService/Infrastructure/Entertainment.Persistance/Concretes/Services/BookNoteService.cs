using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.EntertainmentLogs;
using Entertainment.API.Consts;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.DTOs.BookNoteDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using static System.Reflection.Metadata.BlobBuilder;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookNoteService : IBookNoteService
    {
        private readonly IBookNoteReadRepository _read;
        private readonly IBookNoteWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<BookNoteService> _logger;
        private readonly IDatabase _cache;

        public BookNoteService(IBookNoteWriteRepository bookWriteRepository, IBookNoteReadRepository bookReadRepository, IMapper mapper, ILogger<BookNoteService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateBookNote(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateBookNote(entity.NoteHeader, entity.UserId));
                return _write.Create(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> CreateBookNoteAsync(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateBookNote(entity.NoteHeader, entity.UserId));
                return await _write.CreateAsync(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int DeleteBookNote(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteBookNote(id));
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> DeleteBookNoteAsync(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteBookNote(id));
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<BookNoteDto> GetAllBookNotes()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBookNotes();
                var cachedBookNotes = _cache.StringGet(cacheKey);

                if (!cachedBookNotes.IsNull)
                    return JsonConvert.DeserializeObject<List<BookNoteDto>>(cachedBookNotes);

                var bookNotes = _read.GetAll();

                var serializedBookNotes = JsonConvert.SerializeObject(bookNotes);
                _cache.StringSet(cacheKey, serializedBookNotes);

                _logger.LogInformation(EntertainmentLogs.GetAllBookNotes());
                return _mapper.Map<List<BookNoteDto>>(bookNotes);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<BookNoteDto>> GetAllBookNotesAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBookNotes();
                var cachedBookNotes = await _cache.StringGetAsync(cacheKey);

                if (!cachedBookNotes.IsNull)
                    return JsonConvert.DeserializeObject<List<BookNoteDto>>(cachedBookNotes);

                var bookNotes = await _read.GetAllAsync();

                var serializedBookNotes = JsonConvert.SerializeObject(bookNotes);
                await _cache.StringSetAsync(cacheKey, serializedBookNotes);

                _logger.LogInformation(EntertainmentLogs.GetAllBookNotes());
                return _mapper.Map<List<BookNoteDto>>(bookNotes);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public BookNoteDto GetBookNoteById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetBookNote(id);
                var cachedBookNote = _cache.StringGet(cacheKey);

                if (!cachedBookNote.IsNull)
                    return JsonConvert.DeserializeObject<BookNoteDto>(cachedBookNote);

                var bookNote = _read.GetById(id);

                var serializedBookNote = JsonConvert.SerializeObject(bookNote);
                _cache.StringSet(cacheKey, serializedBookNote);

                _logger.LogInformation(EntertainmentLogs.GetBookNoteById(id));
                return _mapper.Map<BookNoteDto>(bookNote);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<BookNoteDto> GetBookNoteByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetBookNote(id);
                var cachedBookNote = await _cache.StringGetAsync(cacheKey);

                if (!cachedBookNote.IsNull)
                    return JsonConvert.DeserializeObject<BookNoteDto>(cachedBookNote);

                var bookNote = await _read.GetByIdAsync(id);

                var serializedBookNote = JsonConvert.SerializeObject(bookNote);
                await _cache.StringSetAsync(cacheKey, serializedBookNote);

                _logger.LogInformation(EntertainmentLogs.GetBookNoteById(id));
                return _mapper.Map<BookNoteDto>(bookNote);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<BookNoteDto> GetUsersAllBookNotes(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllBookNotes(userId);
                var cachedBookNotes = _cache.StringGet(cacheKey);

                if (!cachedBookNotes.IsNull)
                    return JsonConvert.DeserializeObject<List<BookNoteDto>>(cachedBookNotes);

                var bookNotes = _read.GetUsersAll(userId);

                var serializedBookNotes = JsonConvert.SerializeObject(bookNotes);
                _cache.StringSet(cacheKey, serializedBookNotes);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllBookNotes(userId));
                return _mapper.Map<List<BookNoteDto>>(bookNotes);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<BookNoteDto>> GetUsersAllBookNotesAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllBookNotes(userId);
                var cachedBookNotes = await _cache.StringGetAsync(cacheKey);

                if (!cachedBookNotes.IsNull)
                    return JsonConvert.DeserializeObject<List<BookNoteDto>>(cachedBookNotes);

                var bookNotes = await _read.GetUsersAllAsync(userId);

                var serializedBookNotes = JsonConvert.SerializeObject(bookNotes);
                await _cache.StringSetAsync(cacheKey, serializedBookNotes);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllBookNotes(userId));
                return _mapper.Map<List<BookNoteDto>>(bookNotes);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int UpdateBookNote(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateBookNote(entity.NoteHeader, entity.UserId));
                return _write.Update(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> UpdateBookNoteAsync(BookNoteDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateBookNote(entity.NoteHeader, entity.UserId));
                return await _write.UpdateAsync(_mapper.Map<BookNote>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
