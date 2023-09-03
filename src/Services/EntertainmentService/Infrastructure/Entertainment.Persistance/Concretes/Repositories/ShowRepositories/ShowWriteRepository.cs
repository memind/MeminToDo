using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.ShowRepositories
{
    public class ShowWriteRepository : IShowWriteRepository
    {
        private readonly IDapperBaseWriteRepository _service;

        public ShowWriteRepository(IDapperBaseWriteRepository service)
        {
            _service = service;
        }

        public int Create(Show entity)
        {
            return _service.EditData($"INSERT INTO \"Shows\" (\"Id\", \"ShowName\"  , \"EpisodeCount\", \"IsFinished\", \"UserId\" , \"CreatedDate\") VALUES ('{entity.Id}', '{entity.ShowName}', '{entity.EpisodeCount}', '{entity.IsFinished}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public async Task<int> CreateAsync(Show entity)
        {
            return await _service.EditDataAsync($"INSERT INTO \"Shows\" (\"Id\", \"ShowName\"  , \"EpisodeCount\", \"IsFinished\", \"UserId\" , \"CreatedDate\") VALUES ('{entity.Id}', '{entity.ShowName}', '{entity.EpisodeCount}', '{entity.IsFinished}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public int Delete(string id)
        {
            return _service.EditData($"DELETE FROM \"Shows\" WHERE \"Id\"='{id}'");
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync($"DELETE FROM \"Shows\" WHERE \"Id\"='{id}'");
        }

        public int Update(Show entity)
        {
            return _service.EditData($"UPDATE \"Shows\" SET \"ShowName\" ='{entity.ShowName}', \"EpisodeCount\" ='{entity.EpisodeCount}', \"IsFinished\" ='{entity.IsFinished}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }

        public async Task<int> UpdateAsync(Show entity)
        {
            return await _service.EditDataAsync($"UPDATE \"Shows\" SET \"ShowName\" ='{entity.ShowName}', \"EpisodeCount\" ='{entity.EpisodeCount}', \"IsFinished\" ='{entity.IsFinished}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }
    }
}
