using FSI.Ecommerce.Domain.Aggregates;
using FSI.Ecommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Entities
{
    public class Cart : BaseEntity, IAggregateRoot
    {
        public long? AccountId { get; private set; }
        public string? GuestToken { get; private set; }
        public CartStatus Status { get; private set; } = CartStatus.Open;

        public Account? Account { get; private set; }
        public ICollection<CartItem> Items { get; private set; } = new List<CartItem>();

        private Cart() { }

        public Cart(long? accountId, string? guestToken)
            : base()
        {
            AccountId = accountId;
            GuestToken = guestToken;
        }

        public void AddItem(long productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = Items.FirstOrDefault(i => i.ProductId == productId);

            if (existing is null)
            {
                Items.Add(new CartItem(Id, productId, quantity, unitPrice));
            }
            else
            {
                existing.IncreaseQuantity(quantity);
            }

            Touch();
        }

        public void MarkConverted()
        {
            Status = CartStatus.Converted;
            Touch();
        }

        public void MarkAbandoned()
        {
            Status = CartStatus.Abandoned;
            Touch();
        }
    }
}