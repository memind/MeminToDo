using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.BookNoteRepositories
{
    public class BookNoteWriteRepository : IBookNoteWriteRepository
    {
        private readonly IDapperBaseWriteService _service;

        public BookNoteWriteRepository(IDapperBaseWriteService service)
        {
            _service = service;
        }

        public int Create(BookNote entity)
        {
            return _service.EditData("INSERT INTO \"BookNotes\" (Id, NoteHeader, NoteContent, BookId, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @NoteHeader, @NoteContent, @BookId, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public async Task<int> CreateAsync(BookNote entity)
        {
            return await _service.EditDataAsync("INSERT INTO \"BookNotes\" (Id, NoteHeader, NoteContent, BookId, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @NoteHeader, @NoteContent, @BookId, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public int Delete(string id)
        {
            return _service.EditData("DELETE FROM \"BookNotes\" WHERE Id=@Id", new { id });
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync("DELETE FROM \"BookNotes\" WHERE Id=@Id", new { id });
        }

        public int Update(BookNote entity)
        {
            return _service.EditData("Update \"BookNotes\" SET Id=@Id, NoteHeader=@NoteHeader, NoteContent=@NoteContent, BookId=@BookId, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }

        public async Task<int> UpdateAsync(BookNote entity)
        {
            return await _service.EditDataAsync("Update \"BookNotes\" SET Id=@Id, NoteHeader=@NoteHeader, NoteContent=@NoteContent, BookId=@BookId, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }
    }
}
