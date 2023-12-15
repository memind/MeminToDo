using AutoMapper;
using Common.Logging.Logs.SkillLogs;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Persistance.Concretes.Services
{
    public class ArtService : IArtService
    {
        private readonly IArtReadRepository _read;
        private readonly IArtWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<ArtService> _logger;

        public ArtService(IArtWriteRepository write, IArtReadRepository read, IMapper mapper, ILogger<ArtService> logger)
        {
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
                _logger.LogInformation(SkillLogs.GetAllArts());
                return _read.GetAll();
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Art>> GetAllArtsAsync()
        {
            try
            {
                _logger.LogInformation(SkillLogs.GetAllArts());
                return await _read.GetAllAsync();
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetManyResult<Art> GetUsersAllArts(Guid id)
        {
            try
            {
                _logger.LogInformation(SkillLogs.GetUsersAllArts(id));
                return _read.GetFiltered(x => x.UserId == id);
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetManyResult<Art>> GetAllUsersArtsAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(SkillLogs.GetUsersAllArts(id));
                return await _read.GetFilteredAsync(x => x.UserId == id);
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public GetOneResult<Art> GetArtById(string id)
        {
            try
            {
                _logger.LogInformation(SkillLogs.GetArtById(id));
                return _read.GetById(id);
            } catch (Exception error) { _logger.LogError(SkillLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<GetOneResult<Art>> GetArtByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation(SkillLogs.GetArtById(id));
                return await _read.GetByIdAsync(id);
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
