using TechHaven.DTOs.Public.Order;

namespace TechHaven.Areas.Admin.ViewModels;

public class OrdersIndexViewModel
{
    public IEnumerable<OrderListDto> Orders { get; set; } = null!;  
}
