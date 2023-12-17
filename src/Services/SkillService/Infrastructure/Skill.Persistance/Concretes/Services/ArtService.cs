using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.SkillLogs;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;
using Skill.Persistance.Consts;
using StackExchange.Redis;
using Thrift.Protocol.Entities;

namespace Skill.Persistance.Concretes.Services
{
    public class ArtService : IArtService
    {
        private readonly IArtReadRepository _read;
        private readonly IArtWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<ArtService> _logger;
        private readonly IDatabase _cache;

        public ArtService(IArtWriteRepository write, IArtReadRepository read, IMapper mapper, ILogger<ArtService> logger)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _write = write;
            _read = read;
            _mapper = mapper;
            _logger = logger;
        }

        public GetOneResult<Art> CreateArt(ArtDto newArt, string id)
        {
            try
            {
                newArt.Id = ObjectId.GenerateNewId();
                newArt.UserId = Guid.Parse(id);

                var map = _mapper.Map<Art>(newArt);
                var result = _write.InsertOne(map);

                _logger.LogInformation(SkillLogs.CreateArt(newArt.Name));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Art>> CreateArtAsync(ArtDto newArt, string id)
        {
            try
            {
                newArt.Id = ObjectId.GenerateNewId();
                newArt.UserId = Guid.Parse(id);

                var map = _mapper.Map<Art>(newArt);
                var result = await _write.InsertOneAsync(map);

                _logger.LogInformation(SkillLogs.CreateArt(newArt.Name));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public void DeleteArt(string id)
        {
            try
            {
                _write.DeleteById(id);
                _logger.LogInformation(SkillLogs.DeleteArt(id));
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task DeleteArtAsync(string id)
        {
            try
            {
                await _write.DeleteByIdAsync(id);
                _logger.LogInformation(SkillLogs.DeleteArt(id));
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetManyResult<Art> GetAllArts()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllArts();
                var cachedArts = _cache.StringGet(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Art>>(cachedArts);

                var arts = _read.GetAll();

                var serializedArts = JsonConvert.SerializeObject(arts);
                _cache.StringSet(cacheKey, serializedArts);

                _logger.LogInformation(SkillLogs.GetAllArts());
                return arts;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Art>> GetAllArtsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllArts();
                var cachedArts = await _cache.StringGetAsync(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Art>>(cachedArts);

                var arts = await _read.GetAllAsync();

                var serializedArts = JsonConvert.SerializeObject(arts);
                await _cache.StringSetAsync(cacheKey, serializedArts);

                _logger.LogInformation(SkillLogs.GetAllArts());
                return arts;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetManyResult<Art> GetUsersAllArts(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllArts(id);
                var cachedArts = _cache.StringGet(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Art>>(cachedArts);

                var arts = _read.GetFiltered(x => x.UserId == id);

                var serializedArts = JsonConvert.SerializeObject(arts);
                _cache.StringSet(cacheKey, serializedArts);

                _logger.LogInformation(SkillLogs.GetUsersAllArts(id));
                return arts;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Art>> GetAllUsersArtsAsync(Guid id)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllArts(id);
                var cachedArts = await _cache.StringGetAsync(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetManyResult<Art>>(cachedArts);

                var arts = await _read.GetFilteredAsync(x => x.UserId == id);

                var serializedArts = JsonConvert.SerializeObject(arts);
                await _cache.StringSetAsync(cacheKey, serializedArts);

                _logger.LogInformation(SkillLogs.GetUsersAllArts(id));
                return arts;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetOneResult<Art> GetArtById(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetArtById(id);
                var cachedArts = _cache.StringGet(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetOneResult<Art>>(cachedArts);

                var art = _read.GetById(id);

                var serializedArt = JsonConvert.SerializeObject(art);
                _cache.StringSet(cacheKey, serializedArt);

                _logger.LogInformation(SkillLogs.GetArtById(id));
                return art;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Art>> GetArtByIdAsync(string id)
        {
            try
            {
                var cacheKey = CacheConsts.GetArtById(id);
                var cachedArts = await _cache.StringGetAsync(cacheKey);

                if (!cachedArts.IsNull)
                    return JsonConvert.DeserializeObject<GetOneResult<Art>>(cachedArts);

                var art = await _read.GetByIdAsync(id);

                var serializedArt = JsonConvert.SerializeObject(art);
                await _cache.StringSetAsync(cacheKey, serializedArt);

                _logger.LogInformation(SkillLogs.GetArtById(id));
                return art;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetOneResult<Art> UpdateArt(string id, ArtDto dto)
        {
            try
            {
                var map = _mapper.Map<Art>(dto);
                map.Id = ObjectId.Parse(id);
                var result = _write.ReplaceOne(map, id);

                _logger.LogInformation(SkillLogs.UpdateArt(id));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Art>> UpdateArtAsync(string id, ArtDto dto)
        {
            try
            {
                var map = _mapper.Map<Art>(dto);
                map.Id = ObjectId.Parse(id);
                var result = await _write.ReplaceOneAsync(map, id);

                _logger.LogInformation(SkillLogs.UpdateArt(id));

                return result;
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
