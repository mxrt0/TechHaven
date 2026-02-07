namespace TechHaven.DTOs.Public.Products;

public sealed record ProductListDto(
    int Id,
    string Name,
    decimal Price,
    string ImageUrl,
    string Category
);
