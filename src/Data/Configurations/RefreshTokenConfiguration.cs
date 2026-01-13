using E_Commerce.Data.Configurations;
using E_Commerce.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Dat.Configurations;

public class RefreshTokenConfiguration : AuditableEntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.Property(rt => rt.Token)
            .HasMaxLength(200);

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(x => x.UserId)
            .IsRequired();

        builder.Property(rt => rt.ExpiresOnUtc)
            .IsRequired();
    }
}