using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.EntertainmentLogs;
using Entertainment.API.Consts;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.ShowDTOs;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Entertainment.Persistance.Concretes.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowReadRepository _read;
        private readonly IShowWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<ShowService> _logger;
        private readonly IDatabase _cache;

        public ShowService(IShowWriteRepository bookWriteRepository, IShowReadRepository bookReadRepository, IMapper mapper, ILogger<ShowService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateShow(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateShow(entity.ShowName, entity.UserId));
                return _write.Create(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> CreateShowAsync(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateShow(entity.ShowName, entity.UserId));
                return await _write.CreateAsync(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int DeleteShow(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteShow(id));
                return _write.Delete(id);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> DeleteShowAsync(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteShow(id));
                return await _write.DeleteAsync(id);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ShowDto> GetAllShows()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllShows();
                var cachedShows = _cache.StringGet(cacheKey);

                if (!cachedShows.IsNull)
                    return JsonConvert.DeserializeObject<List<ShowDto>>(cachedShows);

                var shows = _read.GetAll();

                var serializedShows = JsonConvert.SerializeObject(shows);
                _cache.StringSet(cacheKey, serializedShows);

                _logger.LogInformation(EntertainmentLogs.GetAllShows());
                return _mapper.Map<List<ShowDto>>(shows);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ShowDto>> GetAllShowsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllShows();
                var cachedShows = await _cache.StringGetAsync(cacheKey);

                if (!cachedShows.IsNull)
                    return JsonConvert.DeserializeObject<List<ShowDto>>(cachedShows);

                var shows = await _read.GetAllAsync();

                var serializedShows = JsonConvert.SerializeObject(shows);
                await _cache.StringSetAsync(cacheKey, serializedShows);

                _logger.LogInformation(EntertainmentLogs.GetAllShows());
                return _mapper.Map<List<ShowDto>>(shows);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public ShowDto GetShowById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetShow(id);
                var cachedShow = _cache.StringGet(cacheKey);

                if (!cachedShow.IsNull)
                    return JsonConvert.DeserializeObject<ShowDto>(cachedShow);

                var show = _read.GetById(id);

                var serializedShow = JsonConvert.SerializeObject(show);
                _cache.StringSet(cacheKey, serializedShow);

                _logger.LogInformation(EntertainmentLogs.GetShowById(id));
                return _mapper.Map<ShowDto>(show);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ShowDto> GetShowByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetShow(id);
                var cachedShow = await _cache.StringGetAsync(cacheKey);

                if (!cachedShow.IsNull)
                    return JsonConvert.DeserializeObject<ShowDto>(cachedShow);

                var show = await _read.GetByIdAsync(id);

                var serializedShow = JsonConvert.SerializeObject(show);
                await _cache.StringSetAsync(cacheKey, serializedShow);

                _logger.LogInformation(EntertainmentLogs.GetShowById(id));
                return _mapper.Map<ShowDto>(show);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ShowDto> GetUsersAllShows(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllShows(userId);
                var cachedShows = _cache.StringGet(cacheKey);

                if (!cachedShows.IsNull)
                    return JsonConvert.DeserializeObject<List<ShowDto>>(cachedShows);

                var show = _read.GetUsersAll(userId);

                var serializedShows = JsonConvert.SerializeObject(show);
                _cache.StringSet(cacheKey, serializedShows);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllShows(userId));
                return _mapper.Map<List<ShowDto>>(show);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ShowDto>> GetUsersAllShowsAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllShows(userId);
                var cachedShows = await _cache.StringGetAsync(cacheKey);

                if (!cachedShows.IsNull)
                    return JsonConvert.DeserializeObject<List<ShowDto>>(cachedShows);

                var shows = await _read.GetUsersAllAsync(userId);

                var serializedShows = JsonConvert.SerializeObject(shows);
                await _cache.StringSetAsync(cacheKey, serializedShows);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllShows(userId));
                return _mapper.Map<List<ShowDto>>(shows);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int UpdateShow(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateShow(entity.ShowName, entity.UserId));
                return _write.Update(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> UpdateShowAsync(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateShow(entity.ShowName, entity.UserId));
                return await _write.UpdateAsync(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
