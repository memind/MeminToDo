using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.GameRepositories
{
    public class GameWriteRepository : IGameWriteRepository
    {
        private readonly IDapperBaseWriteService _service;

        public GameWriteRepository(IDapperBaseWriteService service)
        {
            _service = service;
        }

        public int Create(Game entity)
        {
            return _service.EditData("INSERT INTO \"Games\" (Id, GameName, Games.Genres, Studio, IsCompleted, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @GameName, @Genres, @Studio, @IsCompleted, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public async Task<int> CreateAsync(Game entity)
        {
            return await _service.EditDataAsync("INSERT INTO \"Games\" (Id, GameName, Games.Genres, Studio, IsCompleted, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @GameName, @Genres, @Studio, @IsCompleted, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public int Delete(string id)
        {
            return _service.EditData("DELETE FROM \"Games\" WHERE Id=@Id", new { id });
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync("DELETE FROM \"Games\" WHERE Id=@Id", new { id });
        }

        public int Update(Game entity)
        {
            return _service.EditData("Update \"Games\" SET Id=@Id, GameName=@GameName, Games.Genres=@Genres, Studio=@Studio, IsCompleted=@IsCompleted UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }

        public async Task<int> UpdateAsync(Game entity)
        {
            return await _service.EditDataAsync("Update \"Games\" SET Id=@Id, GameName=@GameName, Games.Genres=@Genres, Studio=@Studio, IsCompleted=@IsCompleted UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }
    }
}
