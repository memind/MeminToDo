using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Domain.Entities;
using Skill.Persistance.Concretes.Repositories.BaseRepositories;
using Skill.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill.Persistance.Concretes.Repositories.ArtRepositories
{
    public class ArtReadRepository : MongoReadRepositoryBase<Art>, IArtReadRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Art> _collection;
        public ArtReadRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Art>();
        }
    }
}
