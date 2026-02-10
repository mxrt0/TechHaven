using TechHaven.DTOs.Public.Categories;
using TechHaven.DTOs.Public.Products;

namespace TechHaven.Models;

public class ProductsIndexViewModel
{
    public IReadOnlyList<ProductListDto> Products { get; set; } 
        = new List<ProductListDto>();

    public IReadOnlyList<CategoryListDto> Categories { get; set; }
       = new List<CategoryListDto>();

    public string? SearchTerm { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool IsCategoryLocked { get; set; }
}
