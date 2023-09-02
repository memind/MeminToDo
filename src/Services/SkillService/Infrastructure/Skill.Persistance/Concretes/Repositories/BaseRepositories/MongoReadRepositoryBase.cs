using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Skill.Application.Repositories;
using Skill.Domain.Entities.Common;
using Skill.Persistance.Context;
using System.Linq.Expressions;

namespace Skill.Persistance.Concretes.Repositories.BaseRepositories
{
    public class MongoReadRepositoryBase<T> : IReadRepository<T> where T : SkillBase, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;
        public MongoReadRepositoryBase(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<T>();
        }

        public GetManyResult<T> GetFiltered(Expression<Func<T, bool>> filter)
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = _collection.Find(filter).ToList();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<GetManyResult<T>> GetFilteredAsync(Expression<Func<T, bool>> filter)
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = await _collection.Find(filter).ToListAsync();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public GetManyResult<T> GetAll()
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = _collection.AsQueryable().ToList();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<GetManyResult<T>> GetAllAsync()
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = await _collection.AsQueryable().ToListAsync();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public GetOneResult<T> GetById(string id, string type = "object")
        {
            var result = new GetOneResult<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = _collection.Find(filter).FirstOrDefault();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<GetOneResult<T>> GetByIdAsync(string id, string type = "object")
        {
            var result = new GetOneResult<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = await _collection.Find(filter).FirstOrDefaultAsync();
                if (data != null)
                {
                    result.Data = data;
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                result.Success = false;
                result.Data = null;
            }
            return result;
        }
    }
}
