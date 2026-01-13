namespace E_Commerce.Dtos;

public sealed class OrderItemDto
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductDto? Product { get; set; }
}
