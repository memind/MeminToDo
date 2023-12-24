using Skill.Application.DTOs.SongDTOs;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;
using System.Threading;

namespace Skill.Application.Abstractions.Services
{
    public interface ISongService
    {
        public GetManyResult<Song> GetAllSongs();
        public Task<GetManyResult<Song>> GetAllSongsAsync();

        public GetOneResult<Song> GetSongById(string id);
        public Task<GetOneResult<Song>> GetSongByIdAsync(string id);

        public GetManyResult<Song> GetUsersAllSongs(Guid id);
        public Task<GetManyResult<Song>> GetUsersAllSongsAsync(Guid id);

        public GetOneResult<Song> CreateSong(SongDto newSong, string id);
        public Task<GetOneResult<Song>> CreateSongAsync(SongDto newSong, string id);

        public void DeleteSong(string id);
        public Task DeleteSongAsync(string id);

        public GetOneResult<Song> UpdateSong(string id, SongDto dto);
        public Task<GetOneResult<Song>> UpdateSongAsync(string id, SongDto dto);

        public void UploadSong(Guid fileName, string filePath);
        public Task UploadSongAsync(Guid fileName, string filePath);
    }
}
