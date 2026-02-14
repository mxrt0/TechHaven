using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Common;
using TechHaven.DTOs.Public.Cart;
using TechHaven.Models;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cart;
    private readonly IOrderService _orderService;
    public CartController(ICartService cart, IOrderService orderService)
    {
        _cart = cart;
        _orderService = orderService;
    }

    public IActionResult Index()
    {
        var items = _cart.GetCart();
        var vm = new CartIndexViewModel
        {
            Items = items ?? new List<CartItemDto>(),
            TotalPrice = items?.Sum(i => i.Total) ?? 0m
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Add(int productId, int quantity = 1)
    {
        _cart.Add(productId, quantity);
        return Json(new
        {
            success = true,
            message = Messages.AddedToCartMessage
        });
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cart.Remove(productId);
        return Json(new
        {
            success = true,
            message = Messages.RemovedFromCartMessage
        });
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        _cart.UpdateQuantity(productId, quantity);
        return Json(new
        {
            success = true,
            message = Messages.CartUpdatedMessage
        });
    }

    [HttpPost]
    public IActionResult Clear()
    {
        _cart.Clear();
        TempData["SuccessMessage"] = Messages.CartClearedMessage;
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = _cart.GetCart();
        if (cart is null || !cart.Any())
        {
            return RedirectToAction(nameof(Index));
        }
        var vm = new CartIndexViewModel
        {
            Items = cart ?? new List<CartItemDto>(),
            TotalPrice = cart?.Sum(i => i.Total) ?? 0m
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CheckoutConfirm()
    {
        var cartItems = _cart.GetCart();
        if (cartItems is null || !cartItems.Any())
        {
            return RedirectToAction(nameof(Index));
        };

        var success = await _orderService.CreateOrderAsync(cartItems, User);
        if (!success)
        {
            return RedirectToAction("Error", "Home");
        }

        _cart.Clear();
        TempData["SuccessMessage"] = Messages.OrderPlacedMessage;
        return RedirectToAction("Index", "Orders");
    }
}
