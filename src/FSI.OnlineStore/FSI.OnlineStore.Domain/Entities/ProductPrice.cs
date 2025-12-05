using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class ProductPrice
    {
        public uint ProductPriceId { get; private set; }
        public uint ProductId { get; private set; }
        public string PriceType { get; private set; } = string.Empty; // Retail / Wholesale
        public uint MinQuantity { get; private set; }
        public uint? MaxQuantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private ProductPrice() { }

        public ProductPrice(uint productId, string priceType, uint minQuantity, uint? maxQuantity, decimal unitPrice)
        {
            ProductId = productId;
            PriceType = priceType;
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            UnitPrice = unitPrice;
            CreatedAt = DateTime.UtcNow;
        }

        public bool IsInRange(uint quantity)
        {
            if (quantity < MinQuantity) return false;
            if (MaxQuantity.HasValue && quantity > MaxQuantity.Value) return false;
            return true;
        }
    }
}
