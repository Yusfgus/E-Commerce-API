using E_Commerce.Models.Common;

namespace E_Commerce.Models.Payments;

public class PaymentMethod : Entity
{
    public string Name { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; } = [];
}
