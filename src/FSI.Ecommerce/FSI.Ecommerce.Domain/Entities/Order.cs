using FSI.Ecommerce.Domain.Aggregates;
using FSI.Ecommerce.Domain.Enums;
using FSI.Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public string OrderNumber { get; private set; } = null!;
        public long AccountId { get; private set; }
        public long? PlacedByUserId { get; private set; }
        public long? CartId { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Money TotalAmount { get; private set; }

        public string ShippingName { get; private set; } = null!;
        public string ShippingLine1 { get; private set; } = null!;
        public string? ShippingLine2 { get; private set; }
        public string ShippingCity { get; private set; } = null!;
        public string ShippingState { get; private set; } = null!;
        public string ShippingPostalCode { get; private set; } = null!;
        public string ShippingCountryCode { get; private set; } = null!;

        public string BillingName { get; private set; } = null!;
        public string BillingLine1 { get; private set; } = null!;
        public string? BillingLine2 { get; private set; }
        public string BillingCity { get; private set; } = null!;
        public string BillingState { get; private set; } = null!;
        public string BillingPostalCode { get; private set; } = null!;
        public string BillingCountryCode { get; private set; } = null!;

        public Account Account { get; private set; } = null!;
        public User? PlacedByUser { get; private set; }
        public Cart? Cart { get; private set; }
        public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public ICollection<PaymentTransaction> Payments { get; private set; } = new List<PaymentTransaction>();

        private Order() { }

        public Order(
            string orderNumber,
            long accountId,
            long? placedByUserId,
            long? cartId,
            Money initialTotal,
            string shippingName,
            string shippingLine1,
            string? shippingLine2,
            string shippingCity,
            string shippingState,
            string shippingPostalCode,
            string shippingCountryCode,
            string billingName,
            string billingLine1,
            string? billingLine2,
            string billingCity,
            string billingState,
            string billingPostalCode,
            string billingCountryCode)
            : base()
        {
            OrderNumber = orderNumber;
            AccountId = accountId;
            PlacedByUserId = placedByUserId;
            CartId = cartId;
            TotalAmount = initialTotal;

            ShippingName = shippingName;
            ShippingLine1 = shippingLine1;
            ShippingLine2 = shippingLine2;
            ShippingCity = shippingCity;
            ShippingState = shippingState;
            ShippingPostalCode = shippingPostalCode;
            ShippingCountryCode = shippingCountryCode;

            BillingName = billingName;
            BillingLine1 = billingLine1;
            BillingLine2 = billingLine2;
            BillingCity = billingCity;
            BillingState = billingState;
            BillingPostalCode = billingPostalCode;
            BillingCountryCode = billingCountryCode;
        }

        public void AddItem(long productId, string productName, Money unitPrice, int quantity)
        {
            Items.Add(new OrderItem(Id, productId, productName, unitPrice, quantity));
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            if (!Items.Any())
            {
                TotalAmount = Money.Zero(TotalAmount.Currency);
            }
            else
            {
                var currency = Items.First().UnitPrice.Currency;
                var total = Items
                    .Select(i => i.LineTotal)
                    .Aggregate(Money.Zero(currency), (acc, next) => acc.Add(next));

                TotalAmount = total;
            }

            Touch();
        }

        public void MarkPaid()
        {
            Status = OrderStatus.Paid;
            Touch();
        }
    }
}