using FSI.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class BusinessProfileConfiguration : IEntityTypeConfiguration<BusinessProfile>
    {
        public void Configure(EntityTypeBuilder<BusinessProfile> builder)
        {
            builder.ToTable("business_profiles");

            builder.HasKey(p => p.AccountId);

            builder.Property(p => p.AccountId)
                .HasColumnName("account_id");

            builder.Property(p => p.CompanyName)
                .HasColumnName("company_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.TradeName)
                .HasColumnName("trade_name")
                .HasMaxLength(200);

            builder.Property(p => p.TaxId)
                .HasColumnName("tax_id")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.StateRegistration)
                .HasColumnName("state_registration")
                .HasMaxLength(50);

            builder.HasOne(p => p.Account)
                .WithOne(a => a.BusinessProfile)
                .HasForeignKey<BusinessProfile>(p => p.AccountId);
        }
    }
}