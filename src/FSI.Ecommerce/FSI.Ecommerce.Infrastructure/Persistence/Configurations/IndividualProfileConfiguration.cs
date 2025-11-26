using FSI.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class IndividualProfileConfiguration : IEntityTypeConfiguration<IndividualProfile>
    {
        public void Configure(EntityTypeBuilder<IndividualProfile> builder)
        {
            builder.ToTable("individual_profiles");

            builder.HasKey(p => p.AccountId);

            builder.Property(p => p.AccountId)
                .HasColumnName("account_id");

            builder.Property(p => p.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.NationalId)
                .HasColumnName("national_id")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.Account)
                .WithOne(a => a.IndividualProfile)
                .HasForeignKey<IndividualProfile>(p => p.AccountId);
        }
    }
}