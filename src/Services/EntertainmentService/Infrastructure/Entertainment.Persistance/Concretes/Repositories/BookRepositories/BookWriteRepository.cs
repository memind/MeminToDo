using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace Entertainment.Persistance.Concretes.Repositories.BookRepositories
{
    public class BookWriteRepository : IBookWriteRepository
    {
        private readonly IDapperBaseWriteRepository _service;

        public BookWriteRepository(IDapperBaseWriteRepository service)
        {
            _service = service;
        }

        public int Create(Book entity)
        {
            return _service.EditData($"INSERT INTO \"Books\" (\"Id\", \"BookName\"  , \"AuthorName\", \"PageCount\", \"IsFinished\", \"UserId\" , \"CreatedDate\"  ) VALUES ('{entity.Id}', '{entity.BookName}', '{entity.AuthorName}', {entity.PageCount}, {entity.IsFinished}, '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public async Task<int> CreateAsync(Book entity)
        {
            return await _service.EditDataAsync($"INSERT INTO \"Books\" (\"Id\", \"BookName\"  , \"AuthorName\", \"PageCount\", \"IsFinished\", \"UserId\" , \"CreatedDate\"  ) VALUES ('{entity.Id}', '{entity.BookName}', '{entity.AuthorName}', {entity.PageCount}, {entity.IsFinished}, '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public int Delete(string id)
        {
            return _service.EditData($"DELETE FROM \"Books\" WHERE \"Id\"='{id}'");
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync($"DELETE FROM \"Books\" WHERE \"Id\"='{id}'");
        }

        public int Update(Book entity)
        {
            return _service.EditData($"UPDATE \"Books\" SET \"BookName\" ='{entity.BookName}', \"AuthorName\" ='{entity.AuthorName}', \"PageCount\" ='{entity.PageCount}', \"IsFinished\" ='{entity.IsFinished}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }

        public async Task<int> UpdateAsync(Book entity)
        {
            return await _service.EditDataAsync($"UPDATE \"Books\" SET \"BookName\" ='{entity.BookName}', \"AuthorName\" ='{entity.AuthorName}', \"PageCount\" ='{entity.PageCount}', \"IsFinished\" ='{entity.IsFinished}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }
    }
}
