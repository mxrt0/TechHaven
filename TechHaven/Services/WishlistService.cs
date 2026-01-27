using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TechHaven.Data;
using TechHaven.Data.Models;
using TechHaven.Services.Contracts;

namespace TechHaven.Services;

public class WishlistService : IWishlistService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public WishlistService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> IsInWishlistAsync(int productId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        var item = await _context.WishlistItems
            .FindAsync(userId, productId);
        return item is not null;
    }

    public async Task<bool> ToggleAsync(int productId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);

        var item = await _context.WishlistItems
            .FindAsync(userId, productId);

        bool added;
        if (item is null)
        {
            _context.WishlistItems.Add(new WishlistItem
            {
                UserId = userId!,
                ProductId = productId
            });
            added = true;
        }
        else
        {
            _context.WishlistItems.Remove(item);
            added = false;
        }

        await _context.SaveChangesAsync();
        return added;
    }
}
