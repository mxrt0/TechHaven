using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechHaven.Common;
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
