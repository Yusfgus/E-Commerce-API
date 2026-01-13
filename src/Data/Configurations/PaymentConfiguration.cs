using E_Commerce.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations;

public class PaymentConfiguration : AuditableEntityConfiguration<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasIndex(x => new {x.CustomerId, x.OrderId})
            .IsUnique(true);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .HasConversion(
                s => s.ToString(),
                v => Enum.Parse<PaymentStatus>(v)
            )
            .HasDefaultValue(PaymentStatus.Pending);

        builder.HasOne(x => x.PaymentMethod)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PaymentMethodId)
            .IsRequired();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(x => x.OrderId);
    }
}
