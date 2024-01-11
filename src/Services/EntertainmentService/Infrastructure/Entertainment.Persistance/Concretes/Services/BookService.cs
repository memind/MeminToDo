using Autofac.Core;
using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.EntertainmentLogs;
using Common.Messaging.RabbitMQ.Abstract;
using Entertainment.API.Consts;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.BookDTOs;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Domain.Entities;
using Entertainment.Persistance.Consts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Entertainment.Persistance.Concretes.Services
{
    public class BookService : IBookService
    {
        private readonly IBookReadRepository _read;
        private readonly IBookWriteRepository _write;
        private readonly IBookNoteReadRepository _bookNote;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;

        public BookService(IBookWriteRepository bookWriteRepository, IBookReadRepository bookReadRepository, IMapper mapper, IBookNoteReadRepository bookNote, ILogger<BookService> logger, IMessageConsumerService message)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _bookNote = bookNote;
            _logger = logger;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.BookServiceName());
        }

        public int CreateBook(BookDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateBook(entity.BookName, entity.UserId));
                return _write.Create(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> CreateBookAsync(BookDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateBook(entity.BookName, entity.UserId));
                return await _write.CreateAsync(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int DeleteBook(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteBook(id));
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> DeleteBookAsync(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteBook(id));
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<BookDto> GetAllBooks()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBooks();
                var cachedBooks = _cache.StringGet(cacheKey);

                if (!cachedBooks.IsNull)
                    return JsonConvert.DeserializeObject<List<BookDto>>(cachedBooks);

                var notes = _bookNote.GetAll();
                var books = _read.GetAll();

                foreach (var book in books)
                    foreach (var note in notes)
                        if (note.BookId == book.Id && note.UserId == book.UserId)
                            book.BookNotes.Add(note);

                var serializedBooks = JsonConvert.SerializeObject(books);
                _cache.StringSet(cacheKey, serializedBooks);

                _logger.LogInformation(EntertainmentLogs.GetAllBooks());
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBooks();
                var cachedBooks = await _cache.StringGetAsync(cacheKey);

                if (!cachedBooks.IsNull)
                    return JsonConvert.DeserializeObject<List<BookDto>>(cachedBooks);

                var notes = await _bookNote.GetAllAsync();
                var books = await _read.GetAllAsync();

                foreach (var book in books)
                    foreach (var note in notes)
                        if (note.BookId == book.Id && note.UserId == book.UserId)
                            book.BookNotes.Add(note);

                var serializedBooks = JsonConvert.SerializeObject(books);
                await _cache.StringSetAsync(cacheKey, serializedBooks);

                _logger.LogInformation(EntertainmentLogs.GetAllBooks());
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public BookDto GetBookById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetBook(id);
                var cachedBook = _cache.StringGet(cacheKey);

                if (!cachedBook.IsNull)
                    return JsonConvert.DeserializeObject<BookDto>(cachedBook);


                var book = _read.GetById(id);
                var notes = _bookNote.GetUsersAll(book.UserId.ToString());

                foreach (var note in notes)
                    if (book.Id == note.BookId)
                        book.BookNotes.Add(note);
                

                var serializedBook = JsonConvert.SerializeObject(book);
                _cache.StringSet(cacheKey, serializedBook);

                _logger.LogInformation(EntertainmentLogs.GetBookById(id));
                return _mapper.Map<BookDto>(book);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<BookDto> GetBookByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetBook(id);
                var cachedBook = _cache.StringGet(cacheKey);

                if (!cachedBook.IsNull)
                    return JsonConvert.DeserializeObject<BookDto>(cachedBook);


                var book = await _read.GetByIdAsync(id);
                var notes = await _bookNote.GetUsersAllAsync(book.UserId.ToString());

                foreach (var note in notes)
                  if (book.Id == note.BookId)
                        book.BookNotes.Add(note);
                

                var serializedBook = JsonConvert.SerializeObject(book);
                await _cache.StringSetAsync(cacheKey, serializedBook);

                _logger.LogInformation(EntertainmentLogs.GetBookById(id));
                return _mapper.Map<BookDto>(book);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<BookDto> GetUsersAllBooks(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBooks();
                var cachedBooks = _cache.StringGet(cacheKey);

                if (!cachedBooks.IsNull)
                    return JsonConvert.DeserializeObject<List<BookDto>>(cachedBooks);


                var books = _read.GetUsersAll(userId);
                var notes = _bookNote.GetUsersAll(userId);

                foreach (var book in books)
                    foreach (var note in notes)
                        if (book.Id == note.BookId)
                            book.BookNotes.Add(note);

                var serializedBooks = JsonConvert.SerializeObject(books);
                _cache.StringSet(cacheKey, serializedBooks);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllBooks(userId));
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<BookDto>> GetUsersAllBooksAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllBooks();
                var cachedBooks = await _cache.StringGetAsync(cacheKey);

                if (!cachedBooks.IsNull)
                    return JsonConvert.DeserializeObject<List<BookDto>>(cachedBooks);


                var books = await _read.GetUsersAllAsync(userId);
                var notes = await _bookNote.GetUsersAllAsync(userId);

                foreach (var book in books)
                    foreach (var note in notes)
                        if (book.Id == note.BookId)
                            book.BookNotes.Add(note);

                var serializedBooks = JsonConvert.SerializeObject(books);
                await _cache.StringSetAsync(cacheKey, serializedBooks);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllBooks(userId));
                return _mapper.Map<List<BookDto>>(books);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int UpdateBook(BookDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateBook(entity.BookName, entity.UserId));
                return _write.Update(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> UpdateBookAsync(BookDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateBook(entity.BookName, entity.UserId));
                return await _write.UpdateAsync(_mapper.Map<Book>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo();

        public void ConsumeTestInfo() => _message.ConsumeStartTest();
    }
}
