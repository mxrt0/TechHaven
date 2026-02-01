using TechHaven.DTOs.Products;

namespace TechHaven.Models;

public class DetailsViewModel
{
    public bool IsInWishlist { get; set; }
    public bool IsInCart { get; set; }
    public ProductDetailsDto Product { get; set; } = null!;
}
