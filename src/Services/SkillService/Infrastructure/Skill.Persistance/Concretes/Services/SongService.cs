using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.SongDTOs;
using Skill.Application.Repositories.SongRepositories;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Persistance.Concretes.Services
{
    public class SongService : ISongService
    {
        private readonly ISongReadRepository _read;
        private readonly ISongWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<SongService> _logger;

        public SongService(ISongWriteRepository write, ISongReadRepository read, IMapper mapper, ILogger<SongService> logger)
        {
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

                _logger.LogInformation($"Created Song: {newSong.SongName}");

                return result;
            }
            catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GetOneResult<Song>> CreateSongAsync(SongDto newSong, string id)
        {
            try
            {
                newSong.Id = ObjectId.GenerateNewId();
                newSong.UserId = Guid.Parse(id);

                var map = _mapper.Map<Song>(newSong);
                var result = await _write.InsertOneAsync(map);

                _logger.LogInformation($"Created Song: {newSong.SongName}");

                return result;
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public void DeleteSong(string id)
        {
            try
            {
                _write.DeleteById(id);
                _logger.LogInformation($"Deleted Song: {id}");
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task DeleteSongAsync(string id)
        {
            try
            {
                await _write.DeleteByIdAsync(id);
                _logger.LogInformation($"Deleted Song: {id}");
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public GetManyResult<Song> GetAllSongs()
        {
            try
            {
                _logger.LogInformation("Getting All Songs");
                return _read.GetAll();
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GetManyResult<Song>> GetAllSongsAsync()
        {
            try
            {
                _logger.LogInformation("Getting All Songs");
                return await _read.GetAllAsync();
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public GetManyResult<Song> GetUsersAllSongs(Guid id)
        {
            try
            {
                _logger.LogInformation("Getting Users All Songs");
                return _read.GetFiltered(x => x.UserId == id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GetManyResult<Song>> GetUsersAllSongsAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Getting Users All Songs");
                return await _read.GetFilteredAsync(x => x.UserId == id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public GetOneResult<Song> GetSongById(string id)
        {
            try
            {
                _logger.LogInformation($"Getting Song: {id}");
                return _read.GetById(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GetOneResult<Song>> GetSongByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Getting Song: {id}");
                return await _read.GetByIdAsync(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public GetOneResult<Song> UpdateSong(string id, SongDto dto)
        {
            try
            {
                dto.Id = ObjectId.Parse(id);
                var map = _mapper.Map<Song>(dto);
                var result = _write.ReplaceOne(map, id);

                _logger.LogInformation($"Updated Song: {id}");

                return result;
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GetOneResult<Song>> UpdateSongAsync(string id, SongDto dto)
        {
            try
            {
                dto.Id = ObjectId.Parse(id);
                var map = _mapper.Map<Song>(dto);
                var result = await _write.ReplaceOneAsync(map, id);

                _logger.LogInformation($"Updated Song: {id}");

                return result;
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }
    }
}
