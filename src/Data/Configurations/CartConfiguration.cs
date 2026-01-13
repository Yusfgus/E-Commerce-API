using E_Commerce.Models.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations;

public class CartConfiguration : AuditableEntityConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        base.Configure(builder);

        builder.ToTable("Carts");

        builder.Property(x => x.Status)
            .HasConversion(
                s => s.ToString(),
                v => Enum.Parse<CartStatus>(v)
            );

        builder.HasOne(x => x.Customer)
            .WithOne(x => x.Cart)
            .HasForeignKey<Cart>(x => x.CustomerId);

        builder.HasIndex(x => x.CustomerId)
                .IsUnique(true);
    }
}
