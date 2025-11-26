using FSI.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.CartId)
                .HasColumnName("cart_id")
                .IsRequired();

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .HasColumnName("unit_price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(i => i.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasIndex(i => new { i.CartId, i.ProductId }).IsUnique();

            builder.HasOne(i => i.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CartId);

            builder.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
