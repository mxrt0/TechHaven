namespace TechHaven.DTOs.Public.Wishlist;

public sealed record WishlistProductDto(
    int Id,
    string Name,
    decimal Price,
    string ImageUrl,
    bool InStock
);
