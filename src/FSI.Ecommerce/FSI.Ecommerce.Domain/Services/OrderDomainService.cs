using FSI.Ecommerce.Domain.DomainEvents;
using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Services
{
    public sealed class OrderDomainService : IOrderDomainService
    {
        public Order CreateOrderFromCart(Cart cart, Account account, long? userId)
        {
            if (cart is null) throw new ArgumentNullException(nameof(cart));
            if (account is null) throw new ArgumentNullException(nameof(account));

            var currency = "USD"; // pode vir do carrinho ou config
            var total = cart.Items
                .Select(i => new Money(i.UnitPrice * i.Quantity, currency))
                .Aggregate(new Money(0, currency), (acc, next) => new Money(acc.Amount + next.Amount, currency));

            var order = new Order(
                orderNumber: Guid.NewGuid().ToString("N")[..12].ToUpperInvariant(),
                accountId: account.Id,
                placedByUserId: userId,
                cartId: cart.Id,
                initialTotal: total,
                shippingName: account.DisplayName,
                shippingLine1: "TODO: map from address",
                shippingLine2: null,
                shippingCity: "TODO",
                shippingState: "TODO",
                shippingPostalCode: "TODO",
                shippingCountryCode: "US",
                billingName: account.DisplayName,
                billingLine1: "TODO",
                billingLine2: null,
                billingCity: "TODO",
                billingState: "TODO",
                billingPostalCode: "TODO",
                billingCountryCode: "US"
            );

            foreach (var item in cart.Items)
            {
                order.AddItem(
                    productId: item.ProductId,
                    productName: item.Product.Name,
                    unitPrice: new Money(item.UnitPrice, currency),
                    quantity: item.Quantity);
            }

            order.AddDomainEvent(new OrderPlacedDomainEvent(order));
            return order;
        }
    }
}