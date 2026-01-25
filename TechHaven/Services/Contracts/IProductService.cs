using TechHaven.DTOs.Products;

namespace TechHaven.Services.Contracts;

public interface IProductService
{
    Task<IReadOnlyList<ProductListDto>> GetAllAsync();
    Task<IReadOnlyList<ProductListDto>> GetByCategoryAsync(int categoryId);
    Task<ProductDetailsDto?> GetByIdAsync(int id);
}
