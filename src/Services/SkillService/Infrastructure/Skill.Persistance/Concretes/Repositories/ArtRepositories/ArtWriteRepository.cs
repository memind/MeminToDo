using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Domain.Entities;
using Skill.Persistance.Concretes.Repositories.BaseRepositories;
using Skill.Persistance.Context;

namespace Skill.Persistance.Concretes.Repositories.ArtRepositories
{
    public class ArtWriteRepository : MongoWriteRepositoryBase<Art>, IArtWriteRepository
    {
        private readonly SkillDbContext _context;
        private readonly IMongoCollection<Art> _collection;
        public ArtWriteRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new SkillDbContext(settings);
            _collection = _context.GetCollection<Art>();
        }
    }
}
