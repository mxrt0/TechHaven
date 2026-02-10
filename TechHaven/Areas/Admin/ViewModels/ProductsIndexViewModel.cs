using TechHaven.DTOs.Admin.Products;

namespace TechHaven.Areas.Admin.ViewModels;

public class ProductsIndexViewModel
{
    public IEnumerable<AdminProductListDto> Products { get; set; } = null!;
}
