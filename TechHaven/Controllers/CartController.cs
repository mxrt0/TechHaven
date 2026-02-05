using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TechHaven.DTOs.Cart;
using TechHaven.DTOs.Order;
using TechHaven.Models;
using TechHaven.Services.Contracts;

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
            message = "Item(s) added to cart 🛒"
        });
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cart.Remove(productId);
        return Json(new
        {
            success = true,
            message = "Item removed from cart"
        });
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        _cart.UpdateQuantity(productId, quantity);
        return Json(new
        {
            success = true,
            message = "Cart updated"
        });
    }

    [HttpPost]
    public IActionResult Clear()
    {
        _cart.Clear();
        TempData["SuccessMessage"] = "Cart cleared";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = _cart.GetCart();
        if (cart is null || !cart.Any())
        {
            TempData["ErrorMessage"] = "Shopping cart is empty.";
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
            TempData["ErrorMessage"] = "Shopping cart is empty.";
            return RedirectToAction(nameof(Index));
        };

        var success = await _orderService.CreateOrderAsync(cartItems, User);
        if (!success)
        {
            return RedirectToAction("Error", "Home");
        }

        _cart.Clear();
        TempData["SuccessMessage"] = "Your order has been placed.";
        return RedirectToAction("Index", "Orders");
    }
}
