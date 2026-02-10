using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Areas.Admin.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalProducts { get; set; }
    public int ActiveProducts { get; set; }
    public int TotalOrders { get; set; }
    public List<OrderListDto> RecentOrders { get; set; } = new();
    public List<AdminProductListDto> LowStockProducts { get; set; } = new();
}
