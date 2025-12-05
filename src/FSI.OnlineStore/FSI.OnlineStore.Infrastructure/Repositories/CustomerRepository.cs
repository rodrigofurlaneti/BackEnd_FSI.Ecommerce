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
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly MySqlConnectionFactory _connectionFactory;

        public CustomerRepository(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<uint> InsertAsync(Customer customer, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO Customer (
    CustomerTypeId,
    FullName,
    EmailAddress,
    PasswordHash,
    CpfNumber,
    IsActive,
    CreatedAt
)
VALUES (
    @CustomerTypeId,
    @FullName,
    @EmailAddress,
    @PasswordHash,
    @CpfNumber,
    @IsActive,
    @CreatedAt
);
SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            var id = await connection.ExecuteScalarAsync<uint>(sql, new
            {
                customer.CustomerTypeId,
                customer.FullName,
                customer.EmailAddress,
                customer.PasswordHash,
                customer.CpfNumber,
                customer.IsActive,
                customer.CreatedAt
            });
            return id;
        }

        public async Task UpdateAsync(Customer customer, CancellationToken ct)
        {
            const string sql = @"
UPDATE Customer
SET FullName     = @FullName,
    EmailAddress = @EmailAddress,
    IsActive     = @IsActive,
    UpdatedAt    = @UpdatedAt
WHERE CustomerId = @CustomerId;";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                customer.FullName,
                customer.EmailAddress,
                customer.IsActive,
                customer.UpdatedAt,
                customer.CustomerId
            });
        }

        public async Task<Customer?> GetByIdAsync(uint customerId, CancellationToken ct)
        {
            const string sql = @"SELECT CustomerId, CustomerTypeId, FullName, EmailAddress,
                                    PasswordHash, CpfNumber, IsActive, CreatedAt, UpdatedAt
                             FROM Customer
                             WHERE CustomerId = @CustomerId;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { CustomerId = customerId });
        }

        public async Task<Customer?> GetByEmailAsync(string emailAddress, CancellationToken ct)
        {
            const string sql = @"SELECT CustomerId, CustomerTypeId, FullName, EmailAddress,
                                    PasswordHash, CpfNumber, IsActive, CreatedAt, UpdatedAt
                             FROM Customer
                             WHERE EmailAddress = @EmailAddress;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { EmailAddress = emailAddress });
        }

        public async Task<IReadOnlyCollection<Customer>> ListAsync(CancellationToken ct)
        {
            const string sql = @"SELECT CustomerId, CustomerTypeId, FullName, EmailAddress,
                                    PasswordHash, CpfNumber, IsActive, CreatedAt, UpdatedAt
                             FROM Customer;";

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<Customer>(sql);
            return result.ToList();
        }

        public async Task DeleteAsync(uint customerId, CancellationToken ct)
        {
            const string sql = @"DELETE FROM Customer WHERE CustomerId = @CustomerId;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { CustomerId = customerId });
        }
    }
}
