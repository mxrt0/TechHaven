namespace TechHaven.DTOs.Public.Products;

public sealed record ProductCreateDto(
    string Name,
    string Description,
    string SpecsJson,
    decimal Price,
    int StockQuantity,
    int CategoryId,
    string ImageUrl
);
