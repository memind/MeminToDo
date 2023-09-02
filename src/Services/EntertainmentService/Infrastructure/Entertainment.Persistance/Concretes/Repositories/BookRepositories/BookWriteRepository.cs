using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.BookRepositories
{
    public class BookWriteRepository : IBookWriteRepository
    {
        private readonly IDapperBaseWriteService _service;

        public BookWriteRepository(IDapperBaseWriteService service)
        {
            _service = service;
        }

        public int Create(Book entity)
        {
            return _service.EditData("INSERT INTO \"Books\" (Id, BookName, AuthorName, PageCount, IsFinished, Genres, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @BookName, @AuthorName, @PageCount, @IsFinished, @Genres, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public async Task<int> CreateAsync(Book entity)
        {
            return await _service.EditDataAsync("INSERT INTO \"Books\" (Id, BookName, AuthorName, PageCount, IsFinished, Genres, UserId, CreatedDate, UpdatedDate ) VALUES (@Id, @BookName, @AuthorName, @PageCount, @IsFinished, @Genres, @UserId, @CreatedDate, @UpdatedDate)", entity);
        }

        public int Delete(string id)
        {
            return _service.EditData("DELETE FROM \"Books\" WHERE Id=@Id", new { id });
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _service.EditDataAsync("DELETE FROM \"Books\" WHERE Id=@Id", new { id });
        }

        public int Update(Book entity)
        {
            return _service.EditData("Update \"Books\" SET Id=@Id, BookName=@BookName, AuthorName=@AuthorName, PageCount=@PageCount, IsFinished=@IsFinished, Genres=@Genres, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }

        public async Task<int> UpdateAsync(Book entity)
        {
            return await _service.EditDataAsync("Update \"Books\" SET Id=@Id, BookName=@BookName, AuthorName=@AuthorName, PageCount=@PageCount, IsFinished=@IsFinished, Genres=@Genres, UserId=@UserId, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", entity);
        }
    }
}
