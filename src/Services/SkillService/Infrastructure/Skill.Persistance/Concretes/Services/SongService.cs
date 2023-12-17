using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.SkillLogs;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.SongDTOs;
using Skill.Application.Repositories.SongRepositories;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;
using Skill.Persistance.Consts;
using StackExchange.Redis;

namespace Skill.Persistance.Concretes.Services
{
    public class SongService : ISongService
    {
        private readonly ISongReadRepository _read;
        private readonly ISongWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<SongService> _logger;
        private readonly IDatabase _cache;

        public SongService(ISongWriteRepository write, ISongReadRepository read, IMapper mapper, ILogger<SongService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = write;
            _read = read;
            _mapper = mapper;
            _logger = logger;
        }

        public GetOneResult<Song> CreateSong(SongDto newSong, string id)
        {
            try
            {
                newSong.Id = ObjectId.GenerateNewId();
                newSong.UserId = Guid.Parse(id);

                var map = _mapper.Map<Song>(newSong);
                var result = _write.InsertOne(map);

                _logger.LogInformation(SkillLogs.CreateSong(newSong.SongName));

                return result;
            }
            catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Song>> CreateSongAsync(SongDto newSong, string id)
        {
            try
            {
                newSong.Id = ObjectId.GenerateNewId();
                newSong.UserId = Guid.Parse(id);

                var map = _mapper.Map<Song>(newSong);
                var result = await _write.InsertOneAsync(map);

                _logger.LogInformation(SkillLogs.CreateSong(newSong.SongName));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public void DeleteSong(string id)
        {
            try
            {
                _write.DeleteById(id);
                _logger.LogInformation(SkillLogs.DeleteSong(id));
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task DeleteSongAsync(string id)
        {
            try
            {
                await _write.DeleteByIdAsync(id);
                _logger.LogInformation(SkillLogs.DeleteSong(id));
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetManyResult<Song> GetAllSongs()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllSongs();
                var cachedSongs = _cache.StringGet(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Song>>(cachedSongs);

                var songs = _read.GetAll();

                var serializedSongs = JsonConvert.SerializeObject(songs);
                _cache.StringSet(cacheKey, serializedSongs);

                _logger.LogInformation(SkillLogs.GetAllSongs());
                return songs;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Song>> GetAllSongsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllSongs();
                var cachedSongs = await _cache.StringGetAsync(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Song>>(cachedSongs);

                var songs = await _read.GetAllAsync();

                var serializedSongs = JsonConvert.SerializeObject(songs);
                await _cache.StringSetAsync(cacheKey, serializedSongs);

                _logger.LogInformation(SkillLogs.GetAllSongs());
                return songs;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetManyResult<Song> GetUsersAllSongs(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllSongs(id);
                var cachedSongs = _cache.StringGet(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Song>>(cachedSongs);

                var songs = _read.GetFiltered(x => x.UserId == id);

                var serializedSongs = JsonConvert.SerializeObject(songs);
                _cache.StringSet(cacheKey, serializedSongs);

                _logger.LogInformation(SkillLogs.GetUsersAllSongs(id));
                return songs;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Song>> GetUsersAllSongsAsync(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllSongs(id);
                var cachedSongs = await _cache.StringGetAsync(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Song>>(cachedSongs);

                var songs = await _read.GetFilteredAsync(x => x.UserId == id);

                var serializedSongs = JsonConvert.SerializeObject(songs);
                await _cache.StringSetAsync(cacheKey, serializedSongs);

                _logger.LogInformation(SkillLogs.GetUsersAllSongs(id));
                return songs;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetOneResult<Song> GetSongById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetSongById(id);
                var cachedSongs = _cache.StringGet(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetOneResult<Song>>(cachedSongs);

                var song = _read.GetById(id);

                var serializedSong = JsonConvert.SerializeObject(song);
                _cache.StringSet(cacheKey, serializedSong);

                _logger.LogInformation(SkillLogs.GetSongById(id));
                return song;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Song>> GetSongByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetSongById(id);
                var cachedSongs = await _cache.StringGetAsync(cacheKey);

                if (!cachedSongs.IsNull)
                    return JsonConvert.DeserializeObject<GetOneResult<Song>>(cachedSongs);

                var song = await _read.GetByIdAsync(id);

                var serializedSong = JsonConvert.SerializeObject(song);
                await _cache.StringSetAsync(cacheKey, serializedSong);

                _logger.LogInformation(SkillLogs.GetSongById(id));
                return song;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetOneResult<Song> UpdateSong(string id, SongDto dto)
        {
            try
            {
                dto.Id = ObjectId.Parse(id);
                var map = _mapper.Map<Song>(dto);
                var result = _write.ReplaceOne(map, id);

                _logger.LogInformation(SkillLogs.UpdateSong(id));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Song>> UpdateSongAsync(string id, SongDto dto)
        {
            try
            {
                dto.Id = ObjectId.Parse(id);
                var map = _mapper.Map<Song>(dto);
                var result = await _write.ReplaceOneAsync(map, id);

                _logger.LogInformation(SkillLogs.UpdateSong(id));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
