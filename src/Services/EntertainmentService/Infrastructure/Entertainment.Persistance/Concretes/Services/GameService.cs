using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Services
{
    public class GameService : IGameService
    {
        private readonly IGameReadRepository _read;
        private readonly IGameWriteRepository _write;
        private readonly IMapper _mapper;

        public GameService(IGameWriteRepository bookWriteRepository, IGameReadRepository bookReadRepository, IMapper mapper)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
        }

        public int CreateGame(GameDto entity)
        {
            return _write.Create(_mapper.Map<Game>(entity));
        }

        public async Task<int> CreateGameAsync(GameDto entity)
        {
            return await _write.CreateAsync(_mapper.Map<Game>(entity));
        }

        public int DeleteGame(string id)
        {
            return _write.Delete(id);
        }

        public async Task<int> DeleteGameAsync(string id)
        {
            return await _write.DeleteAsync(id);
        }

        public List<GameDto> GetAllGames()
        {
            var games = _read.GetAll();
            return _mapper.Map<List<GameDto>>(games);
        }

        public async Task<List<GameDto>> GetAllGamesAsync()
        {
            var games = await _read.GetAllAsync();
            return _mapper.Map<List<GameDto>>(games);
        }

        public GameDto GetGameById(string id)
        {
            var game = _read.GetById(id);
            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> GetGameByIdAsync(string id)
        {
            var game = await _read.GetByIdAsync(id);
            return _mapper.Map<GameDto>(game);
        }

        public List<GameDto> GetUsersAllGames(string userId)
        {
            var games = _read.GetUsersAll(userId);
            return _mapper.Map<List<GameDto>>(games);
        }

        public async Task<List<GameDto>> GetUsersAllGamesAsync(string userId)
        {
            var games = await _read.GetUsersAllAsync(userId);
            return _mapper.Map<List<GameDto>>(games);
        }

        public int UpdateGame(GameDto entity)
        {
            return _write.Update(_mapper.Map<Game>(entity));
        }

        public async Task<int> UpdateGameAsync(GameDto entity)
        {
            return await _write.UpdateAsync(_mapper.Map<Game>(entity));
        }
    }
}
