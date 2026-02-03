namespace TechHaven.DTOs.Order;

public sealed record OrderListDto(
Guid OrderId,
string OrderNumber,
DateTime OrderDate,
List<ProductSaleListDto> OrderItems
)
{
    public decimal OrderTotal => OrderItems.Sum(item => item.TotalPrice);
}
