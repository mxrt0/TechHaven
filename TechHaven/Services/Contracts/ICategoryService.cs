using TechHaven.DTOs.Categories;

namespace TechHaven.Services.Contracts;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryListDto>> GetAllAsync();
    Task<CategoryListDto?> GetByIdAsync(int id);
}
