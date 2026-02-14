using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Common;
using TechHaven.DTOs.Public.Order;
using TechHaven.Models;
using TechHaven.Services.Contracts.Public;

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
            TempData["ErrorMessage"] = Messages.OrderDoesNotExistOrNoPermissionMessage;
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
        TempData["SuccessMessage"] = string.Format(Messages.OrderCancelledMessage, id.ToString()[..8]);
        return RedirectToAction(nameof(Index));
    }
}
