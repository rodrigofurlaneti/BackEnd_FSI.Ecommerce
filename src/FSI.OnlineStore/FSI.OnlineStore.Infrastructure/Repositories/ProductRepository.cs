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
    public sealed class ProductRepository : IProductRepository
    {
        private readonly MySqlConnectionFactory _connectionFactory;

        public ProductRepository(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<uint> InsertAsync(Product product, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO Product (ProductName, SkuCode, BasePrice, IsActive, CreatedAt)
VALUES (@ProductName, @SkuCode, @BasePrice, @IsActive, @CreatedAt);
SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<uint>(sql, new
            {
                product.ProductName,
                product.SkuCode,
                product.BasePrice,
                product.IsActive,
                product.CreatedAt
            });
        }

        public async Task UpdateAsync(Product product, CancellationToken ct)
        {
            const string sql = @"
UPDATE Product
SET ProductName = @ProductName,
    SkuCode     = @SkuCode,
    BasePrice   = @BasePrice,
    IsActive    = @IsActive,
    UpdatedAt   = @UpdatedAt
WHERE ProductId = @ProductId;";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                product.ProductName,
                product.SkuCode,
                product.BasePrice,
                product.IsActive,
                product.UpdatedAt,
                product.ProductId
            });
        }

        public async Task<Product?> GetByIdAsync(uint productId, CancellationToken ct)
        {
            const string sql = @"SELECT ProductId, ProductName, SkuCode, BasePrice, IsActive, CreatedAt, UpdatedAt
                             FROM Product
                             WHERE ProductId = @ProductId;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { ProductId = productId });
        }

        public async Task<IReadOnlyCollection<Product>> ListAsync(CancellationToken ct)
        {
            const string sql = @"SELECT ProductId, ProductName, SkuCode, BasePrice, IsActive, CreatedAt, UpdatedAt
                             FROM Product;";

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<Product>(sql);
            return result.ToList();
        }

        public async Task DeleteAsync(uint productId, CancellationToken ct)
        {
            const string sql = @"DELETE FROM Product WHERE ProductId = @ProductId;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { ProductId = productId });
        }

        public async Task<IReadOnlyCollection<ProductPrice>> ListPricesByProductAsync(uint productId, CancellationToken ct)
        {
            const string sql = @"SELECT ProductPriceId, ProductId, PriceType, MinQuantity, MaxQuantity,
                                    UnitPrice, CreatedAt, UpdatedAt
                             FROM ProductPrice
                             WHERE ProductId = @ProductId;";

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<ProductPrice>(sql, new { ProductId = productId });
            return result.ToList();
        }
    }
}
