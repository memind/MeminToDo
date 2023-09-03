using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.BookNoteRepositories
{
    internal class BookNoteReadRepository : IBookNoteReadRepository
    {
        private readonly IDapperBaseReadRepository _service;

        public BookNoteReadRepository(IDapperBaseReadRepository service)
        {
            _service = service;
        }

        public List<BookNote> GetAll()
        {
            var bookNoteList = _service.GetAll<BookNote>("SELECT * FROM \"BookNotes\"");
            return bookNoteList;
        }

        public async Task<List<BookNote>> GetAllAsync()
        {
            var bookNoteList = await _service.GetAllAsync<BookNote>("SELECT * FROM \"BookNotes\"");
            return bookNoteList;
        }

        public BookNote GetById(string id)
        {
            var bookNote = _service.Get<BookNote>($"SELECT * FROM \"BookNotes\" WHERE \"Id\" = '{id}'");
            return bookNote;
        }

        public async Task<BookNote> GetByIdAsync(string id)
        {
            var bookNote = await _service.GetAsync<BookNote>($"SELECT * FROM \"BookNotes\" WHERE \"Id\" = '{id}'");
            return bookNote;
        }

        public List<BookNote> GetUsersAll(string userId)
        {
            var bookNoteList = _service.GetAll<BookNote>($"SELECT * FROM \"BookNotes\" WHERE \"UserId\" = '{userId}'");
            return bookNoteList;
        }

        public async Task<List<BookNote>> GetUsersAllAsync(string userId)
        {
            var bookNoteList = await _service.GetAllAsync<BookNote>($"SELECT * FROM \"BookNotes\" WHERE \"UserId\" = '{userId}'");
            return bookNoteList;
        }
    }
}
