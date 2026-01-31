namespace TechHaven.DTOs.Cart;

public sealed record CartItemDto(
    int ProductId,
    string Name,
    decimal Price,
    decimal Total,
    int Quantity);

