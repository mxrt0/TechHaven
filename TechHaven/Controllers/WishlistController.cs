using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Common;
using TechHaven.Models;
using TechHaven.Services.Contracts;

namespace TechHaven.Controllers;


public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var items = await _wishlistService.GetByUserIdAsync(User);
        var vm = new WishlistIndexViewModel
        {
            Wishlist = items.ToArray()
        };
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Toggle(int productId)
    {
        bool added = await _wishlistService.ToggleAsync(productId, User);
        return Json(new
        {
            added,
            message = added
                    ? Messages.AddedToWishlistMessage
                    : Messages.RemovedFromWishlistMessage
        });
    }

}
