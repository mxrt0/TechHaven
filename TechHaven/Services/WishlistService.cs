using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechHaven.Data;
using TechHaven.Data.Models;
using TechHaven.DTOs.Wishlist;
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

    public async Task<IEnumerable<WishlistProductDto>> GetByUserIdAsync(ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);

        return await _context.WishlistItems
            .Where(wi => wi.UserId == userId)
            .Include(wi => wi.Product)
            .Where(wi => wi.Product.IsActive)
            .OrderByDescending(wi => wi.AddedAt)
            .Select(wi => new WishlistProductDto
            (
                wi.ProductId,
                wi.Product.Name,
                wi.Product.Price,
                wi.Product.ImageUrl,
                wi.Product.StockQuantity > 0
            ))
            .ToListAsync();

    }

    public async Task<bool> IsInWishlistAsync(int productId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        var item = await _context.WishlistItems
            .Include(wi => wi.Product)
             .FirstOrDefaultAsync(wi => wi.UserId == userId && wi.ProductId == productId && wi.Product.IsActive);
        return item is not null;
    }

    public async Task<bool> ToggleAsync(int productId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);

        var product = await _context.Products
            .Where(p => p.Id == productId && p.IsActive)
            .FirstOrDefaultAsync();

        if (product is null)
            return false;

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
