using Microsoft.AspNetCore.Mvc.Rendering;
using TechHaven.DTOs.Admin.Products;

namespace TechHaven.Areas.Admin.ViewModels;

public class ProductCreateViewModel
{
    public SelectList? Categories { get; set; }
    public AdminProductCreateDto Product { get; set; } = null!;
}
