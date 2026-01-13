using E_Commerce.Data.Configurations;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Dat.Configurations;

public class UserConfiguration : AuditableEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(p => p.Email)
            .IsRequired();

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.PasswordHash)
            .HasColumnName("Password")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired(false);

        builder.Property(x => x.Role)
            .HasConversion(
                x => x.ToString(),
                v => Enum.Parse<UserRole>(v)
            )
            .IsRequired();
    }
}
