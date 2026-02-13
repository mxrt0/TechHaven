using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Categories;

namespace TechHaven.Areas.Admin.ViewModels;

public class ProductsIndexViewModel
{
    public string? SearchTerm { get; set; }

    public int? CategoryId { get; set; }

    public StockFilter Stock { get; set; } = StockFilter.All;

    public ProductSort SortBy { get; set; } = ProductSort.Newest;

    public IEnumerable<CategoryListDto> Categories { get; set; } = new List<CategoryListDto>();

    public IEnumerable<AdminProductListDto> Products { get; set; } = null!;

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
