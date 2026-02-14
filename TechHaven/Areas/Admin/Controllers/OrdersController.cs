using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Areas.Admin.ViewModels;
using TechHaven.Services.Contracts.Admin;

namespace TechHaven.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    public const int PageSize = 10;
    private readonly IAdminOrderService _orderService;

    public OrdersController(IAdminOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index(OrdersIndexViewModel filterVm, int page = 1)
    {
        if (page < 1) page = 1;
        var (orders, totalItems) = await _orderService.SearchAsync(filterVm.SearchTerm, filterVm.SortBy, page, PageSize);

        ViewData["ActivePage"] = "Orders";
        var vm = new OrdersIndexViewModel
        {
            Orders = orders,
            SearchTerm = filterVm.SearchTerm,
            SortBy = filterVm.SortBy,
            Page = page,
            PageSize = OrdersController.PageSize,
            TotalItems = totalItems
        };
        return View(vm);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null) return NotFound();

        var vm = new OrderDetailsViewModel
        {
            Order = order
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var result = await _orderService.CancelOrderAsync(id);
        if (!result) return NotFound();

        TempData["SuccessMessage"] = "Order cancelled successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> MarkAsShipped(Guid id)
    {
        var result = await _orderService.MarkAsShippedAsync(id);
        if (!result) 
        { 
            return RedirectToAction("Error", "Home", new { area = "", message = "Error marking order as shipped." });
        }
        TempData["SuccessMessage"] = "Order marked as shipped.";
        return RedirectToAction(nameof(Index));
    }

}
