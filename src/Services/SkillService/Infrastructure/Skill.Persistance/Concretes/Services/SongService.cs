using AutoMapper;
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

        public SongService(ISongWriteRepository write, ISongReadRepository read, IMapper mapper)
        {
            _write = write;
            _read = read;
            _mapper = mapper;
        }

        public GetOneResult<Song> CreateSong(SongDto newSong, string id)
        {
            newSong.Id = ObjectId.GenerateNewId();
            newSong.UserId = Guid.Parse(id);
            var map = _mapper.Map<Song>(newSong);
            var result = _write.InsertOne(map);

            return result;
        }

        public async Task<GetOneResult<Song>> CreateSongAsync(SongDto newSong, string id)
        {
            newSong.Id = ObjectId.GenerateNewId();
            newSong.UserId = Guid.Parse(id);
            var map = _mapper.Map<Song>(newSong);
            var result = await _write.InsertOneAsync(map);

            return result;
        }

        public void DeleteSong(string id)
        {
            _write.DeleteById(id);
        }

        public async Task DeleteSongAsync(string id)
        {
            await _write.DeleteByIdAsync(id);
        }

        public GetManyResult<Song> GetAllSongs()
        {
            return _read.GetAll();
        }

        public async Task<GetManyResult<Song>> GetAllSongsAsync()
        {
            return await _read.GetAllAsync();
        }

        public GetManyResult<Song> GetUsersAllSongs(Guid id)
        {
            return _read.GetFiltered(x => x.UserId == id);
        }

        public async Task<GetManyResult<Song>> GetUsersAllSongsAsync(Guid id)
        {
            return await _read.GetFilteredAsync(x => x.UserId == id);
        }

        public GetOneResult<Song> GetSongById(string id)
        {
            return _read.GetById(id);
        }

        public async Task<GetOneResult<Song>> GetSongByIdAsync(string id)
        {
            return await _read.GetByIdAsync(id);
        }

        public GetOneResult<Song> UpdateSong(string id, SongDto dto)
        {
            dto.Id = ObjectId.Parse(id);
            var map = _mapper.Map<Song>(dto);
            var result = _write.ReplaceOne(map, id);
            return result;
        }

        public async Task<GetOneResult<Song>> UpdateSongAsync(string id, SongDto dto)
        {
            dto.Id = ObjectId.Parse(id);
            var map = _mapper.Map<Song>(dto);
            var result = await _write.ReplaceOneAsync(map, id);
            return result;
        }
    }
}
