namespace TechHaven.DTOs.Products;

public sealed record ProductDetailsDto(
    int Id,
    string Name,
    string Description,
    Dictionary<string, string> Specs,
    decimal Price,
    int StockQuantity,
    string ImageUrl,
    string Category
);

