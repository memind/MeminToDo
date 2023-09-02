using Dapper;
using Entertainment.Application.Repositories.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Entertainment.Persistance.Concretes.Repositories.Common
{
    public class DapperBaseReadRepository : IDapperBaseReadService
    {
        private readonly IDbConnection _db;

        public DapperBaseReadRepository(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public T Get<T>(string command, object parms)
        {
            T result;
            result = _db.Query<T>(command, parms).FirstOrDefault();

            return result;
        }

        public async Task<T> GetAsync<T>(string command, object parms)
        {
            T result;

            result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

            return result;
        }

        public List<T> GetAll<T>(string command, object parms)
        {
            List<T> result = new List<T>();

            result = _db.Query<T>(command, parms).ToList();
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>(string command, object parms)
        {
            List<T> result = new List<T>();

            result = (await _db.QueryAsync<T>(command, parms)).ToList();
            return result;
        }
    }
}
