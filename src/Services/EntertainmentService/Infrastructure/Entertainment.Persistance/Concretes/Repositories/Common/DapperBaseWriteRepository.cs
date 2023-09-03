using Dapper;
using Entertainment.Application.Repositories.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Entertainment.Persistance.Concretes.Repositories.Common
{
    public class DapperBaseWriteRepository : IDapperBaseWriteRepository
    {
        private readonly IDbConnection _db;

        public DapperBaseWriteRepository(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public int EditData(string command)
        {
            int result;

            result = _db.Execute(command);

            return result;
        }

        public async Task<int> EditDataAsync(string command)
        {
            int result;

            result = await _db.ExecuteAsync(command);

            return result;
        }
    }
}
