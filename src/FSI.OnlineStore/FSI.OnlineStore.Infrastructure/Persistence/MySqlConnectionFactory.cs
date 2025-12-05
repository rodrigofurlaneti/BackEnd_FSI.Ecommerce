using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FSI.OnlineStore.Infrastructure.Persistence
{
    public sealed class MySqlConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OnlineStoreDatabase")
                ?? throw new InvalidOperationException("Connection string 'OnlineStoreDatabase' not configured.");
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
