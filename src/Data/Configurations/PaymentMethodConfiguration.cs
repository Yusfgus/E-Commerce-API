using E_Commerce.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations;

public class PaymentMethodConfiguration : EntityConfiguration<PaymentMethod>
{
    public override void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        base.Configure(builder);

        builder.ToTable("PaymentMethods");

        builder.Property(x => x.Name)
            .HasMaxLength(10)
            .IsRequired();
    }
}