using TechHaven.DTOs.Categories;

namespace TechHaven.Models;

public class HomeIndexViewModel
{
    public IReadOnlyList<CategoryListDto> Categories { get; set; } = [];
}
