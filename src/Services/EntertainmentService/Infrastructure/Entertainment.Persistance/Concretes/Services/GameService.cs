﻿using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Entertainment.Persistance.Concretes.Services
{
    public class GameService : IGameService
    {
        private readonly IGameReadRepository _read;
        private readonly IGameWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameWriteRepository bookWriteRepository, IGameReadRepository bookReadRepository, IMapper mapper, ILogger<GameService> logger)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateGame(GameDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Game: {entity.GameName} - {entity.UserId}");
                return _write.Create(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> CreateGameAsync(GameDto entity)
        {
            try
            {
                _logger.LogInformation($"Created Game: {entity.GameName} - {entity.UserId}");
                return await _write.CreateAsync(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int DeleteGame(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Game: {id}");
                return _write.Delete(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> DeleteGameAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleted Game: {id}");
                return await _write.DeleteAsync(id);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<GameDto> GetAllGames()
        {
            try
            {
                var games = _read.GetAll();

                _logger.LogInformation("Getting All Games");

                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<GameDto>> GetAllGamesAsync()
        {
            try
            {
                var games = await _read.GetAllAsync();

                _logger.LogInformation("Getting All Games");

                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public GameDto GetGameById(string id)
        {
            try
            {
                var game = _read.GetById(id);

                _logger.LogInformation($"Getting Game: {id}");

                return _mapper.Map<GameDto>(game);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<GameDto> GetGameByIdAsync(string id)
        {
            try
            {
                var game = await _read.GetByIdAsync(id);

                _logger.LogInformation($"Getting Game: {id}");

                return _mapper.Map<GameDto>(game);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<GameDto> GetUsersAllGames(string userId)
        {
            try
            {
                var games = _read.GetUsersAll(userId);

                _logger.LogInformation("Getting Users All Games");

                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<GameDto>> GetUsersAllGamesAsync(string userId)
        {
            try
            {
                var games = await _read.GetUsersAllAsync(userId);

                _logger.LogInformation("Getting Users All Games");

                return _mapper.Map<List<GameDto>>(games);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public int UpdateGame(GameDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Game: {entity.GameName}");
                return _write.Update(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<int> UpdateGameAsync(GameDto entity)
        {
            try
            {
                _logger.LogInformation($"Updated Game: {entity.GameName}");
                return await _write.UpdateAsync(_mapper.Map<Game>(entity));
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }
    }
}
