using Dapper;
using Entertainment.Application.Repositories.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Entertainment.Persistance.Concretes.Repositories.Common
{
    public class DapperBaseReadRepository : IDapperBaseReadRepository
    {
        private readonly IDbConnection _db;

        public DapperBaseReadRepository(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public T Get<T>(string command)
        {
            T result;
            result = _db.Query<T>(command).FirstOrDefault();

            return result;
        }

        public async Task<T> GetAsync<T>(string command)
        {
            T result;

            result = (await _db.QueryAsync<T>(command).ConfigureAwait(false)).FirstOrDefault();

            return result;
        }

        public List<T> GetAll<T>(string command)
        {
            List<T> result = new List<T>();

            result = _db.Query<T>(command).ToList();
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>(string command)
        {
            List<T> result = new List<T>();

            result = (await _db.QueryAsync<T>(command)).ToList();
            return result;
        }
    }
}
