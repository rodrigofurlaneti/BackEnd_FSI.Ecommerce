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
    public sealed class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly MySqlConnectionFactory _connectionFactory;

        public ShoppingCartRepository(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ShoppingCart?> GetByIdAsync(uint cartId, CancellationToken ct)
        {
            return await LoadCartAsync("CartId = @CartId", new { CartId = cartId });
        }

        public async Task<ShoppingCart?> GetByCustomerAsync(uint customerId, CancellationToken ct)
        {
            return await LoadCartAsync("CustomerId = @CustomerId", new { CustomerId = customerId });
        }

        public async Task<ShoppingCart?> GetByVisitorTokenAsync(string visitorToken, CancellationToken ct)
        {
            return await LoadCartAsync("VisitorToken = @VisitorToken", new { VisitorToken = visitorToken });
        }

        private async Task<ShoppingCart?> LoadCartAsync(string whereClause, object param)
        {
            const string cartSql = @"SELECT CartId, CustomerId, VisitorToken, CartStatus, CreatedAt, ExpiresAt
                                 FROM ShoppingCart
                                 WHERE {WHERE}
                                 ORDER BY CreatedAt DESC
                                 LIMIT 1;";

            using var connection = _connectionFactory.CreateConnection();

            var sql = cartSql.Replace("{WHERE}", whereClause);
            var cartRow = await connection.QuerySingleOrDefaultAsync(sql, param);

            if (cartRow is null) return null;

            var cart = new ShoppingCart(
                (uint?)cartRow.CustomerId,
                (string?)cartRow.VisitorToken,
                (DateTime?)cartRow.ExpiresAt
            );

            typeof(ShoppingCart)
                .GetProperty("CartId")!
                .SetValue(cart, (uint)cartRow.CartId);
            typeof(ShoppingCart)
                .GetProperty("CartStatus")!
                .SetValue(cart, (string)cartRow.CartStatus);
            typeof(ShoppingCart)
                .GetProperty("CreatedAt")!
                .SetValue(cart, (DateTime)cartRow.CreatedAt);

            const string itemsSql = @"SELECT CartItemId, CartId, ProductId, Quantity, UnitPrice, CreatedAt
                                  FROM CartItem
                                  WHERE CartId = @CartId;";

            var items = await connection.QueryAsync<CartItem>(itemsSql, new { CartId = cartRow.CartId });

            var listField = typeof(ShoppingCart)
                .GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var list = (List<CartItem>)listField!.GetValue(cart)!;
            list.AddRange(items);

            return cart;
        }

        public async Task<uint> InsertAsync(ShoppingCart cart, CancellationToken ct)
        {
            const string sql = @"
INSERT INTO ShoppingCart (CustomerId, VisitorToken, CartStatus, CreatedAt, ExpiresAt)
VALUES (@CustomerId, @VisitorToken, @CartStatus, @CreatedAt, @ExpiresAt);
SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<uint>(sql, new
            {
                cart.CustomerId,
                cart.VisitorToken,
                cart.CartStatus,
                cart.CreatedAt,
                cart.ExpiresAt
            });
        }

        public async Task UpdateAsync(ShoppingCart cart, CancellationToken ct)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var transaction = connection.BeginTransaction();

            const string updateCartSql = @"
UPDATE ShoppingCart
SET CartStatus = @CartStatus,
    ExpiresAt  = @ExpiresAt
WHERE CartId   = @CartId;";

            await connection.ExecuteAsync(updateCartSql, new
            {
                cart.CartStatus,
                cart.ExpiresAt,
                cart.CartId
            }, transaction);

            const string deleteItemsSql = @"DELETE FROM CartItem WHERE CartId = @CartId;";
            await connection.ExecuteAsync(deleteItemsSql, new { cart.CartId }, transaction);

            const string insertItemSql = @"
INSERT INTO CartItem (CartId, ProductId, Quantity, UnitPrice, CreatedAt)
VALUES (@CartId, @ProductId, @Quantity, @UnitPrice, @CreatedAt);";

            foreach (var item in cart.Items)
            {
                await connection.ExecuteAsync(insertItemSql, new
                {
                    CartId = cart.CartId,
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.CreatedAt
                }, transaction);
            }

            transaction.Commit();
        }

        public async Task DeleteAsync(uint cartId, CancellationToken ct)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync("DELETE FROM CartItem WHERE CartId = @CartId;", new { CartId = cartId }, transaction);
            await connection.ExecuteAsync("DELETE FROM ShoppingCart WHERE CartId = @CartId;", new { CartId = cartId }, transaction);

            transaction.Commit();
        }
    }
}
