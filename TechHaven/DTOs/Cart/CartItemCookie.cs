namespace TechHaven.DTOs.Cart;

public sealed record CartItemCookie(
    int ProductId,
    int Quantity)
{
    public int ProductId { get; set; } = ProductId;
    public int Quantity { get; set; } = Quantity;
}

