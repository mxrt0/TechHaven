using TechHaven.DTOs.Public.Wishlist;

namespace TechHaven.Models;

public class WishlistIndexViewModel
{
    public ICollection<WishlistProductDto> Wishlist { get; set; } = new List<WishlistProductDto>();
}
