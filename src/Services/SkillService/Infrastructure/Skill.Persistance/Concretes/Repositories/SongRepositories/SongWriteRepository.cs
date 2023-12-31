﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Skill.Application.Repositories.SongRepositories;
using Skill.Domain.Entities;
using Skill.Persistance.Concretes.Repositories.BaseRepositories;
using Skill.Persistance.Context;

namespace Skill.Persistance.Concretes.Repositories.SongRepositories
{
    public class SongWriteRepository : MongoWriteRepositoryBase<Song>, ISongWriteRepository
    {
        private readonly SkillDbContext _context;
        private readonly IMongoCollection<Song> _collection;
        public SongWriteRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new SkillDbContext(settings);
            _collection = _context.GetCollection<Song>();
        }
    }
}
