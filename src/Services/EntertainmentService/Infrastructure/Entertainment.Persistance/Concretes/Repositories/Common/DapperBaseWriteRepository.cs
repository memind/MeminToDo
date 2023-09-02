using Dapper;
using Entertainment.Application.Repositories.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Entertainment.Persistance.Concretes.Repositories.Common
{
    public class DapperBaseWriteRepository : IDapperBaseWriteService
    {
        private readonly IDbConnection _db;

        public DapperBaseWriteRepository(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public int EditData(string command, object parms)
        {
            int result;

            result = _db.Execute(command, parms);

            return result;
        }

        public async Task<int> EditDataAsync(string command, object parms)
        {
            int result;

            result = await _db.ExecuteAsync(command, parms);

            return result;
        }
    }
}
