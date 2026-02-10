namespace TechHaven.DTOs.Public.Order;

public sealed record ProductSaleListDto(
string ProductName,
decimal UnitPrice,
int Quantity,
string ImageUrl
)
{
    public decimal TotalPrice => UnitPrice * Quantity;
}
