using Dashboard.Aggregator.Extensions;
using Dashboard.Aggregator.Models.SkillModels;
using Dashboard.Aggregator.Services.Abstractions;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class SkillService : ISkillService
    {
        private readonly HttpClient _client;

        public SkillService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetTotalArtCount()
        {
            var artResponse = await _client.GetAsync($"/api/getAllArts");
            var arts = await artResponse.ReadContentAs<ArtResponseModel>();

            return arts.Result.Data.Count();
        }

        public async Task<int> GetTotalSongCount()
        {
            var songResponse = await _client.GetAsync($"/api/getAllSongs");
            var songs = await songResponse.ReadContentAs<SongResponseModel>();

            return songs.Result.Data.Count();
        }

        public async Task<int> GetUsersArtCount(string id)
        {
            var artResponse = await _client.GetAsync($"/api/getUsersAllArts?id={id}");
            var arts = await artResponse.ReadContentAs<ArtResponseModel>();

            return arts.Result.Data.Count();
        }

        public async Task<int> GetUsersSongCount(string id)
        {
            var songResponse = await _client.GetAsync($"/api/getUsersAllSongs?id={id}");
            var songs = await songResponse.ReadContentAs<SongResponseModel>();

            return songs.Result.Data.Count();
        }
    }
}
