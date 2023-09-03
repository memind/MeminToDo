using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Entertainment.Persistance.Concretes.Repositories.BookRepositories
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly IDapperBaseReadRepository _service;

        public BookReadRepository(IDapperBaseReadRepository service)
        {
            _service = service;
        }

        public List<Book> GetAll()
        {
            var bookList = _service.GetAll<Book>("SELECT * FROM \"Books\"");
            return bookList;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var bookList = await _service.GetAllAsync<Book>("SELECT * FROM \"Books\"");
            return bookList;
        }

        public Book GetById(string id)
        {
            var book = _service.Get<Book>($"SELECT * FROM \"Books\" WHERE \"Id\" = '{id}'");
            return book;
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            var book = await _service.GetAsync<Book>($"SELECT * FROM \"Books\" WHERE \"Id\" = '{id}'");
            return book;
        }

        public List<Book> GetUsersAll(string userId)
        {
            var bookList = _service.GetAll<Book>($"SELECT * FROM \"Books\" WHERE \"UserId\" = '{userId}'");
            return bookList;
        }

        public async Task<List<Book>> GetUsersAllAsync(string userId)
        {
            var bookList = await _service.GetAllAsync<Book>($"SELECT * FROM \"Books\" WHERE \"UserId\" = '{userId}'");
            return bookList;
        }
    }
}
