namespace E_Commerce.Requests.Cart;

public class AddCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}