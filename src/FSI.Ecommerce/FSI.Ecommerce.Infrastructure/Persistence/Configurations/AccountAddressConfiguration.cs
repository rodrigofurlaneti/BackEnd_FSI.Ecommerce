using FSI.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class AccountAddressConfiguration : IEntityTypeConfiguration<AccountAddress>
    {
        public void Configure(EntityTypeBuilder<AccountAddress> builder)
        {
            builder.ToTable("account_addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(a => a.Label)
                .HasColumnName("label")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Line1)
                .HasColumnName("line1")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.Line2)
                .HasColumnName("line2")
                .HasMaxLength(255);

            builder.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(a => a.State)
                .HasColumnName("state")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(a => a.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.CountryCode)
                .HasColumnName("country_code")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(a => a.IsDefaultShipping)
                .HasColumnName("is_default_shipping");

            builder.Property(a => a.IsDefaultBilling)
                .HasColumnName("is_default_billing");

            builder.Property(a => a.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasOne(a => a.Account)
                .WithMany(ac => ac.Addresses)
                .HasForeignKey(a => a.AccountId);
        }
    }
}