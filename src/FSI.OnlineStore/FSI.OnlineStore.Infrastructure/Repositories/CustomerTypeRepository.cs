using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using FSI.OnlineStore.Domain;
using FSI.OnlineStore.Domain.Entities;
using FSI.OnlineStore.Domain.Repositories;
using FSI.OnlineStore.Infrastructure.Persistence;

namespace FSI.OnlineStore.Infrastructure.Repositories
{
    public sealed class CustomerTypeRepository : ICustomerTypeRepository
    {
        private readonly MySqlConnectionFactory _connectionFactory;

        public CustomerTypeRepository(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IReadOnlyCollection<CustomerType>> ListAsync(CancellationToken ct)
        {
            const string sql = @"SELECT CustomerTypeId, TypeName, CreatedAt, UpdatedAt
                             FROM CustomerType;";

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<CustomerType>(sql);
            return result.ToList();
        }

        public async Task<CustomerType?> GetByNameAsync(string typeName, CancellationToken ct)
        {
            const string sql = @"SELECT CustomerTypeId, TypeName, CreatedAt, UpdatedAt
                             FROM CustomerType
                             WHERE TypeName = @TypeName;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<CustomerType>(sql, new { TypeName = typeName });
        }
    }
}
