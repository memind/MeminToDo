﻿using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.GameRepositories
{
    public class GameReadRepository : IGameReadRepository
    {
        private readonly IDapperBaseReadRepository _service;

        public GameReadRepository(IDapperBaseReadRepository service)
        {
            _service = service;
        }
        public List<Game> GetAll()
        {
            var gameList = _service.GetAll<Game>("SELECT * FROM \"Games\"");
            return gameList;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            var gameList = await _service.GetAllAsync<Game>("SELECT * FROM \"Games\"");
            return gameList;
        }

        public Game GetById(string id)
        {
            var game = _service.Get<Game>($"SELECT * FROM \"Games\" WHERE \"Id\" = '{id}'");
            return game;
        }

        public async Task<Game> GetByIdAsync(string id)
        {
            var game = await _service.GetAsync<Game>($"SELECT * FROM \"Games\" WHERE \"Id\" = '{id}'");
            return game;
        }

        public List<Game> GetUsersAll(string userId)
        {
            var gameList = _service.GetAll<Game>($"SELECT * FROM \"Games\" WHERE \"UserId\" = '{userId}'");
            return gameList;
        }

        public async Task<List<Game>> GetUsersAllAsync(string userId)
        {
            var gameList = await _service.GetAllAsync<Game>($"SELECT * FROM \"Games\" WHERE \"UserId\" = '{userId}'");
            return gameList;
        }
    }
}
