using E_Commerce.Models.Auth;
using E_Commerce.Models.Common;

namespace E_Commerce.Models.Carts;

public sealed class Cart : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public CartStatus Status { get; set; }
    
    // Navigation
    public Customer Customer { get; set; } = null!;
    public ICollection<CartItem> Items { get; set; } = [];
}
