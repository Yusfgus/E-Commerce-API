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

        builder.HasData(SeedData());
    }

    private ICollection<Category> SeedData() => 
    [
        new(){Id = new Guid("2f573015-b1e0-49e9-aad8-9f0afa7a7d73"), Name = "Electronics"},
        new(){Id = new Guid("30a626aa-ab4e-43d8-a5ab-0b1b9b22bce3"), Name = "Clothing"},
        new(){Id = new Guid("4e7d91c8-5cf4-42cd-96bb-d6b888b87e5b"), Name = "Books"},
    ];
}