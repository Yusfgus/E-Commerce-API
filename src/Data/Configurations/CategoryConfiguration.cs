using E_Commerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations;

public class CategoryConfiguration : EntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories");

        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}