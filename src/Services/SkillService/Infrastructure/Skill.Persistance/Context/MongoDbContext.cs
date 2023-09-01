﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Skill.Persistance.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _db = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(typeof(T).Name.Trim());
        }

        public IMongoDatabase GetDatabase()
        {
            return _db;
        }
    }
}
