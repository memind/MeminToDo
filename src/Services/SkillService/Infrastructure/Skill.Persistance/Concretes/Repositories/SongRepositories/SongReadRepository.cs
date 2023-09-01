using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skill.Application.Repositories.SongRepositories;
using Skill.Domain.Entities;
using Skill.Persistance.Concretes.Repositories.BaseRepositories;
using Skill.Persistance.Context;

namespace Skill.Persistance.Concretes.Repositories.SongRepositories
{
    public class SongReadRepository : MongoReadRepositoryBase<Song>, ISongReadRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Song> _collection;
        public SongReadRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Song>();
        }
    }
}
