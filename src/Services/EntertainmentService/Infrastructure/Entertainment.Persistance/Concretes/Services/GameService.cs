using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.EntertainmentLogs;
using Entertainment.API.Consts;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Entertainment.Persistance.Concretes.Services
{
    public class GameService : IGameService
    {
        private readonly IGameReadRepository _read;
        private readonly IGameWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<GameService> _logger;
        private readonly IDatabase _cache;

        public GameService(IGameWriteRepository bookWriteRepository, IGameReadRepository bookReadRepository, IMapper mapper, ILogger<GameService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateGame(GameDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateGame(entity.GameName, entity.UserId));
                return _write.Create(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> CreateGameAsync(GameDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateGame(entity.GameName, entity.UserId));
                return await _write.CreateAsync(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int DeleteGame(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteGame(id));
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> DeleteGameAsync(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteGame(id));
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<GameDto> GetAllGames()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllGames();
                var cachedGames = _cache.StringGet(cacheKey);

                if (!cachedGames.IsNull)
                    return JsonConvert.DeserializeObject<List<GameDto>>(cachedGames);

                var games = _read.GetAll();

                var serializedGames = JsonConvert.SerializeObject(games);
                _cache.StringSet(cacheKey, serializedGames);

                _logger.LogInformation(EntertainmentLogs.GetAllGames());
                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<GameDto>> GetAllGamesAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllGames();
                var cachedGames = await _cache.StringGetAsync(cacheKey);

                if (!cachedGames.IsNull)
                    return JsonConvert.DeserializeObject<List<GameDto>>(cachedGames);

                var games = await _read.GetAllAsync();

                var serializedGames = JsonConvert.SerializeObject(games);
                await _cache.StringSetAsync(cacheKey, serializedGames);

                _logger.LogInformation(EntertainmentLogs.GetAllGames());
                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GameDto GetGameById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetGame(id);
                var cachedGame = _cache.StringGet(cacheKey);

                if (!cachedGame.IsNull)
                    return JsonConvert.DeserializeObject<GameDto>(cachedGame);

                var game = _read.GetById(id);

                var serializedGame = JsonConvert.SerializeObject(game);
                _cache.StringSet(cacheKey, serializedGame);

                _logger.LogInformation(EntertainmentLogs.GetGameById(id));
                return _mapper.Map<GameDto>(game);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GameDto> GetGameByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetGame(id);
                var cachedGame = await _cache.StringGetAsync(cacheKey);

                if (!cachedGame.IsNull)
                    return JsonConvert.DeserializeObject<GameDto>(cachedGame);

                var game = await _read.GetByIdAsync(id);

                var serializedGame = JsonConvert.SerializeObject(game);
                await _cache.StringSetAsync(cacheKey, serializedGame);

                _logger.LogInformation(EntertainmentLogs.GetGameById(id));
                return _mapper.Map<GameDto>(game);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<GameDto> GetUsersAllGames(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllGames(userId);
                var cachedGames = _cache.StringGet(cacheKey);

                if (!cachedGames.IsNull)
                    return JsonConvert.DeserializeObject<List<GameDto>>(cachedGames);

                var game = _read.GetUsersAll(userId);

                var serializedGames = JsonConvert.SerializeObject(game);
                _cache.StringSet(cacheKey, serializedGames);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllGames(userId));
                return _mapper.Map<List<GameDto>>(game);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<GameDto>> GetUsersAllGamesAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllGames(userId);
                var cachedGames = await _cache.StringGetAsync(cacheKey);

                if (!cachedGames.IsNull)
                    return JsonConvert.DeserializeObject<List<GameDto>>(cachedGames);

                var games = await _read.GetUsersAllAsync(userId);

                var serializedGames = JsonConvert.SerializeObject(games);
                await _cache.StringSetAsync(cacheKey, serializedGames);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllGames(userId));
                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int UpdateGame(GameDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateGame(entity.GameName, entity.UserId));
                return _write.Update(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> UpdateGameAsync(GameDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateGame(entity.GameName, entity.UserId));
                return await _write.UpdateAsync(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
