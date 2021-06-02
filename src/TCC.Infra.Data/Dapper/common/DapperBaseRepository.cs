using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Dapper;
namespace TCC.Infra.Data.Dapper.common
{
    public class DapperBaseRepository
    {
        private readonly IConfiguration configuration;
        private const string nameConnectionString = "TccContext";
        public DapperBaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected void OpenConnection(Action<SqlConnection> action)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString(nameConnectionString)))
            {
                connection.Open();
                action?.Invoke(connection);
            }
        }

        protected IEnumerable<T> ExecuteQuery<T>(string sql, object paramter = null)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString(nameConnectionString)))
            {
                connection.Open();
                return connection.Query<T>(sql, paramter);
            }
        }
    }
}
