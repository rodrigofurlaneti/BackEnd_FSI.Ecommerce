using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Infrastructure.Persistence.Configurations
{
    public sealed class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
    {
        public void Configure(EntityTypeBuilder<AccountUser> builder)
        {
            builder.ToTable("account_users");

            builder.HasKey(au => au.Id);

            builder.Property(au => au.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(au => au.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(au => au.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(au => au.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            builder.Property(au => au.IsDefaultAccount)
                .HasColumnName("is_default_account")
                .IsRequired();

            builder.Property(au => au.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.HasIndex(au => new { au.AccountId, au.UserId })
                .IsUnique();

            builder.HasOne(au => au.Account)
                .WithMany(a => a.AccountUsers)
                .HasForeignKey(au => au.AccountId);

            builder.HasOne(au => au.User)
                .WithMany(u => u.AccountUsers)
                .HasForeignKey(au => au.UserId);

            builder.HasOne(au => au.Role)
                .WithMany()
                .HasForeignKey(au => au.RoleId);
        }
    }
}