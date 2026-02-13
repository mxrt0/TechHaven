using TechHaven.Data.Enums;

namespace TechHaven.DTOs.Public.Order;

public sealed record OrderListDto(
Guid OrderId,
string OrderNumber,
DateTime OrderDate,
OrderStatus Status,
List<ProductSaleListDto> OrderItems
)
{
    public decimal OrderTotal => OrderItems.Sum(item => item.TotalPrice);
}
