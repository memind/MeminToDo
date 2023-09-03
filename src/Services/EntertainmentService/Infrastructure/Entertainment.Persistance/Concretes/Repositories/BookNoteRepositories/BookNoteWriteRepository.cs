using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.BookNoteRepositories
{
    public class BookNoteWriteRepository : IBookNoteWriteRepository
    {
        private readonly IDapperBaseWriteRepository _service;

        public BookNoteWriteRepository(IDapperBaseWriteRepository service)
        {
            _service = service;
        }

        public int Create(BookNote entity)
        {
            return _service.EditData($"INSERT INTO \"BookNotes\" (\"Id\", \"NoteHeader\"  , \"NoteContent\", \"BookId\", \"UserId\" , \"CreatedDate\"  ) VALUES ('{entity.Id}', '{entity.NoteHeader}', '{entity.NoteContent}', '{entity.BookId}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public async Task<int> CreateAsync(BookNote entity)
        {
            return await _service.EditDataAsync($"INSERT INTO \"BookNotes\" (\"Id\", \"NoteHeader\"  , \"NoteContent\", \"BookId\", \"UserId\" , \"CreatedDate\"  ) VALUES ('{entity.Id}', '{entity.NoteHeader}', '{entity.NoteContent}', '{entity.BookId}', '{entity.UserId}', '{entity.CreatedDate}')\r\n");
        }

        public int Delete(string id)
        {
            return _service.EditData($"DELETE FROM \"BookNotes\" WHERE \"Id\"='{id}'");
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync($"DELETE FROM \"BookNotes\" WHERE \"Id\"='{id}'");
        }

        public int Update(BookNote entity)
        {
            return _service.EditData($"UPDATE \"BookNotes\" SET \"NoteHeader\" ='{entity.NoteHeader}', \"NoteContent\" ='{entity.NoteContent}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }

        public async Task<int> UpdateAsync(BookNote entity)
        {
            return await _service.EditDataAsync($"UPDATE \"BookNotes\" SET \"NoteHeader\" ='{entity.NoteHeader}', \"NoteContent\" ='{entity.NoteContent}', \"UpdatedDate\" ='{DateTime.Now}' WHERE \"Id\"='{entity.Id}'");
        }
    }
}
