using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.OrderNumber)
                .HasColumnName("order_number")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(o => o.PlacedByUserId)
                .HasColumnName("placed_by_user_id");

            builder.Property(o => o.CartId)
                .HasColumnName("cart_id");

            builder.Property(o => o.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.OwnsOne(o => o.TotalAmount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Property(o => o.ShippingName)
                .HasColumnName("shipping_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(o => o.ShippingLine1)
                .HasColumnName("shipping_line1")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.ShippingLine2)
                .HasColumnName("shipping_line2")
                .HasMaxLength(255);

            builder.Property(o => o.ShippingCity)
                .HasColumnName("shipping_city")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.ShippingState)
                .HasColumnName("shipping_state")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.ShippingPostalCode)
                .HasColumnName("shipping_postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.ShippingCountryCode)
                .HasColumnName("shipping_country_code")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(o => o.BillingName)
                .HasColumnName("billing_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(o => o.BillingLine1)
                .HasColumnName("billing_line1")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.BillingLine2)
                .HasColumnName("billing_line2")
                .HasMaxLength(255);

            builder.Property(o => o.BillingCity)
                .HasColumnName("billing_city")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.BillingState)
                .HasColumnName("billing_state")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.BillingPostalCode)
                .HasColumnName("billing_postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.BillingCountryCode)
                .HasColumnName("billing_country_code")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
        }
    }
}
