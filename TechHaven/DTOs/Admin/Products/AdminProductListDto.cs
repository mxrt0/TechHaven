namespace TechHaven.DTOs.Admin.Products;

public sealed record AdminProductListDto
(
    int Id,
    string Name,
    decimal Price,
    int StockQuantity,
    bool IsActive,
    string CategoryName
);
