using Skill.Application.DTOs.SongDTOs;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Abstractions.Services
{
    public interface ISongService
    {
        public GetManyResult<Song> GetAllSongs();
        public Task<GetManyResult<Song>> GetAllSongsAsync();

        public GetOneResult<Song> GetSongById(string id);
        public Task<GetOneResult<Song>> GetSongByIdAsync(string id);

        public GetOneResult<Song> CreateSong(SongDto newSong);
        public Task<GetOneResult<Song>> CreateSongAsync(SongDto newSong);

        public void DeleteSong(string id);
        public Task DeleteSongAsync(string id);

        public GetOneResult<Song> UpdateSong(string id, SongDto dto);
        public Task<GetOneResult<Song>> UpdateSongAsync(string id, SongDto dto);
    }
}
