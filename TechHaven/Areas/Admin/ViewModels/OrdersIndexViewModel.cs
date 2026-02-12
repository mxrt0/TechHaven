using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Areas.Admin.ViewModels;

public class OrdersIndexViewModel
{
    public string? SearchTerm { get; set; }
    public OrderSort SortBy { get; set; } = OrderSort.Newest;
    public IEnumerable<OrderListDto> Orders { get; set; } = null!;  
}
