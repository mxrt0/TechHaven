using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Areas.Admin.ViewModels;

public class OrdersIndexViewModel
{
    public string? SearchTerm { get; set; }
    public OrderSort SortBy { get; set; } = OrderSort.Newest;
    public IEnumerable<OrderListDto> Orders { get; set; } = null!;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool OnlyPending { get; set; }
}
