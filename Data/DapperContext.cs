using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PosAPI.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<(T, List<TT>)> GetAsyncMultiple<T, TT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = CreateConnection();
            var data = await db.QueryMultipleAsync(sp, parms, commandType: commandType);
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            return (res1, res2);
        }
    }
}
