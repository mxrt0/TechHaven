using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Services.Contracts.Admin;

namespace TechHaven.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly IAdminOrderService _orderService;

    public OrdersController(IAdminOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllAsync();

        ViewData["ActivePage"] = "Orders";
        return View(orders);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null) return NotFound();

        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var result = await _orderService.CancelOrderAsync(id);
        if (!result) return NotFound();

        TempData["SuccessMessage"] = "Order cancelled successfully.";
        return RedirectToAction(nameof(Index));
    }

}
