using FSI.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(i => i.ProductName)
                .HasColumnName("product_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(i => i.UnitPrice, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("unit_price")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.OwnsOne(i => i.LineTotal, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("line_total")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        }
    }
}
