using System.Security.Claims;

namespace TechHaven.Services.Contracts;

public interface IWishlistService
{
    Task<bool> ToggleAsync(int productId, ClaimsPrincipal principal);
    Task<bool> IsInWishlistAsync(int productId, ClaimsPrincipal principal);
}
