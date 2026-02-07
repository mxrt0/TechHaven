using System.Security.Claims;
using TechHaven.DTOs.Public.Wishlist;

namespace TechHaven.Services.Contracts.Public;

public interface IWishlistService
{
    Task<bool> ToggleAsync(int productId, ClaimsPrincipal principal);
    Task<bool> IsInWishlistAsync(int productId, ClaimsPrincipal principal);
    Task<IEnumerable<WishlistProductDto>> GetByUserIdAsync(ClaimsPrincipal principal);
}
