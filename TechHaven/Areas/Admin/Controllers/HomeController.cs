using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Areas.Admin.ViewModels;
using TechHaven.Services.Contracts.Admin;

namespace TechHaven.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly IAdminProductService _productService;
    private readonly IAdminOrderService _orderService;

    public HomeController(
        IAdminProductService productService,
        IAdminOrderService orderService)
    {
        _productService = productService;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var allProducts = await _productService.GetAllAsync();
        var allOrders = await _orderService.GetAllAsync();

        var recentOrders = allOrders
           .OrderByDescending(o => o.OrderDate)
           .Take(5)
           .ToList();

        var lowStockProducts = allProducts
            .Where(p => p.StockQuantity <= 5)
            .OrderBy(p => p.StockQuantity)
            .ToList();

        var model = new AdminDashboardViewModel
        {
            TotalProducts = allProducts.Count,
            ActiveProducts = allProducts.Count(p => p.IsActive),
            TotalOrders = allOrders.Count,
            RecentOrders = recentOrders,
            LowStockProducts = lowStockProducts
        };

        ViewData["ActivePage"] = "Dashboard";
        return View(model);
    }
}
