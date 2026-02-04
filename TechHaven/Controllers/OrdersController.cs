using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.DTOs.Order;
using TechHaven.Models;
using TechHaven.Services.Contracts;

namespace TechHaven.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrdersByUserIdAsync(User);
        var vm = new OrderIndexViewModel
        {
            Orders = orders
        };
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id, User);
        if (order is null)
        {
            TempData["ErrorMessage"] = "Order does not exist or you have no permission to access it.";
            return RedirectToAction("Error", "Home");
        }
        var vm = new OrderDetailsViewModel
        {
            Order = order
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var success = await _orderService.CancelOrderAsync(id, User);
        if (!success)
        {
            return RedirectToAction("Error", "Home");
        }
        TempData["SuccessMessage"] = $"Order #{id.ToString()[..8]} has been cancelled!";
        return RedirectToAction(nameof(Index));
    }
}
