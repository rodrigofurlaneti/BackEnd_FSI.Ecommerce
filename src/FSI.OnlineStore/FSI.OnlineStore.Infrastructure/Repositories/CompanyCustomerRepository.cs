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
    public sealed class CompanyCustomerRepository : ICompanyCustomerRepository
    {
        private readonly MySqlConnectionFactory _connectionFactory;

        public CompanyCustomerRepository(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<uint> InsertAsync(CompanyCustomer company, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO CompanyCustomer (
    CustomerId,
    CorporateName,
    TradeName,
    CnpjNumber,
    StateRegistration,
    CreatedAt
)
VALUES (
    @CustomerId,
    @CorporateName,
    @TradeName,
    @CnpjNumber,
    @StateRegistration,
    @CreatedAt
);
SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            var id = await connection.ExecuteScalarAsync<uint>(sql, new
            {
                company.CustomerId,
                company.CorporateName,
                company.TradeName,
                company.CnpjNumber,
                company.StateRegistration,
                company.CreatedAt
            });
            return id;
        }

        public async Task UpdateAsync(CompanyCustomer company, CancellationToken ct)
        {
            const string sql = @"
UPDATE CompanyCustomer
SET CorporateName     = @CorporateName,
    TradeName         = @TradeName,
    StateRegistration = @StateRegistration,
    UpdatedAt         = @UpdatedAt
WHERE CompanyCustomerId = @CompanyCustomerId;";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                company.CorporateName,
                company.TradeName,
                company.StateRegistration,
                company.UpdatedAt,
                company.CompanyCustomerId
            });
        }

        public async Task<CompanyCustomer?> GetByCustomerIdAsync(uint customerId, CancellationToken ct)
        {
            const string sql = @"SELECT CompanyCustomerId, CustomerId, CorporateName, TradeName,
                                    CnpjNumber, StateRegistration, CreatedAt, UpdatedAt
                             FROM CompanyCustomer
                             WHERE CustomerId = @CustomerId;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<CompanyCustomer>(sql, new { CustomerId = customerId });
        }
    }
}
