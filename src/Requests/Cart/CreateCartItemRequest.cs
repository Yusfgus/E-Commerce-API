namespace E_Commerce.Requests.Cart;

public class CreateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}