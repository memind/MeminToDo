using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.ShowRepositories
{
    public class ShowWriteRepository : IShowWriteRepository
    {
        private readonly IDapperBaseWriteService _service;

        public ShowWriteRepository(IDapperBaseWriteService service)
        {
            _service = service;
        }

        public int Create(Show entity)
        {
            return _service.EditData("INSERT INTO \"Shows\" (Id, ShowName, EpisodeCount, IsFinished, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @ShowName, @EpisodeCount, @IsFinished, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public async Task<int> CreateAsync(Show entity)
        {
            return await _service.EditDataAsync("INSERT INTO \"Shows\" (Id, ShowName, EpisodeCount, IsFinished, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @ShowName, @EpisodeCount, @IsFinished, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public int Delete(string id)
        {
            return _service.EditData("DELETE FROM \"Shows\" WHERE Id=@Id", new { id });
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync("DELETE FROM \"Shows\" WHERE Id=@Id", new { id });
        }

        public int Update(Show entity)
        {
            return _service.EditData("Update \"Shows\" SET Id=@Id, ShowName=@ShowName, EpisodeCount=@EpisodeCount, IsFinished=@IsFinished, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }

        public async Task<int> UpdateAsync(Show entity)
        {
            return await _service.EditDataAsync("Update \"Shows\" SET Id=@Id, ShowName=@ShowName, EpisodeCount=@EpisodeCount, IsFinished=@IsFinished, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }
    }
}
