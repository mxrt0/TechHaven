namespace TechHaven.DTOs.Products;

public sealed record ProductDetailsDto(
    int Id,
    string Name,
    string Description,
    string SpecsJson,
    decimal Price,
    int StockQuantity,
    string ImageUrl,
    string Category
);

