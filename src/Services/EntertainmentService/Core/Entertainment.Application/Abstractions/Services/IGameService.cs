using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Abstractions.Services
{
    public interface IGameService
    {
        int CreateGame(GameDto entity);
        Task<int> CreateGameAsync(GameDto entity);

        int DeleteGame(string id);
        Task<int> DeleteGameAsync(string id);

        int UpdateGame(GameDto entity);
        Task<int> UpdateGameAsync(GameDto entity);

        public List<GameDto> GetAllGames();
        Task<List<GameDto>> GetAllGamesAsync();

        GameDto GetGameById(string id);
        Task<GameDto> GetGameByIdAsync(string id);

        List<GameDto> GetUsersAllGames(string userId);
        Task<List<GameDto>> GetUsersAllGamesAsync(string userId);

        public void ConsumeBackUpInfo();
        public void ConsumeTestInfo();
    }
}
