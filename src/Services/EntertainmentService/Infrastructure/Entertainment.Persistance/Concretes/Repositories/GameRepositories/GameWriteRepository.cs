using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.GameRepositories
{
    public class GameWriteRepository : IGameWriteRepository
    {
        private readonly IDapperBaseWriteRepository _service;

        public GameWriteRepository(IDapperBaseWriteRepository service)
        {
            _service = service;
        }

        public int Create(Game entity)
        {
            return _service.EditData($"INSERT INTO \"Games\" (\"Id\", \"GameName\"  , \"Studio\", \"IsCompleted\", \"UserId\" , \"CreatedDate\") VALUES ('{entity.Id}', '{entity.GameName}', '{entity.Studio}', '{entity.IsCompleted}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public async Task<int> CreateAsync(Game entity)
        {
            return await _service.EditDataAsync($"INSERT INTO \"Games\" (\"Id\", \"GameName\"  , \"Studio\", \"IsCompleted\", \"UserId\" , \"CreatedDate\") VALUES ('{entity.Id}', '{entity.GameName}', '{entity.Studio}', '{entity.IsCompleted}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public int Delete(string id)
        {
            return _service.EditData($"DELETE FROM \"Games\" WHERE \"Id\"='{id}'");
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync($"DELETE FROM \"Games\" WHERE \"Id\"='{id}'");
        }

        public int Update(Game entity)
        {
            return _service.EditData($"UPDATE \"Games\" SET \"GameName\" ='{entity.GameName}', \"Studio\" ='{entity.Studio}', \"IsCompleted\" ='{entity.IsCompleted}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }

        public async Task<int> UpdateAsync(Game entity)
        {
            return await _service.EditDataAsync($"UPDATE \"Games\" SET \"GameName\" ='{entity.GameName}', \"Studio\" ='{entity.Studio}', \"IsCompleted\" ='{entity.IsCompleted}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }
    }
}
