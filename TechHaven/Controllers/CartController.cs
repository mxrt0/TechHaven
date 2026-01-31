using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.DTOs.Cart;
using TechHaven.Models;
using TechHaven.Services.Contracts;

namespace TechHaven.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cart;
    public CartController(ICartService cart)
    {
        _cart = cart;
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
        TempData["SuccessMessage"] = "Item added to cart 🛒";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cart.Remove(productId);
        TempData["SuccessMessage"] = "Item removed from cart";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        _cart.UpdateQuantity(productId, quantity);
        TempData["SuccessMessage"] = "Cart updated";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Clear()
    {
        _cart.Clear();
        TempData["SuccessMessage"] = "Cart cleared";
        return RedirectToAction(nameof(Index));
    }


}
