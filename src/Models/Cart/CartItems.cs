using E_Commerce.Models.Products;

namespace E_Commerce.Models.Carts;

public class CartItem
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    // Navigation
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}