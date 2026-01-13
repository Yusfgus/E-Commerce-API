using E_Commerce.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations;

public class OrderConfiguration : AuditableEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable("Orders");

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);

        builder.Property(x => x.OrderStatus)
            .HasConversion(
                s => s.ToString(),
                v => Enum.Parse<OrderStatus>(v)
            )
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}