using TechHaven.DTOs.Public.Categories;

namespace TechHaven.Services.Contracts.Public;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryListDto>> GetAllAsync();
    Task<CategoryListDto?> GetByIdAsync(int id);
}
