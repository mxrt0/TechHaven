using TechHaven.DTOs.Public.Products;

namespace TechHaven.Services.Contracts.Public;

public interface IProductService
{
    Task<IReadOnlyList<ProductListDto>> GetAllAsync();
    Task<IReadOnlyList<ProductListDto>> GetByCategoryAsync(int categoryId);
    Task<ProductDetailsDto?> GetByIdAsync(int id);
    Task<(IReadOnlyList<ProductListDto>, int totalItems)> SearchAsync(
    string? searchTerm,
    int? categoryId,
    decimal? minPrice,
    decimal? maxPrice,
    int? page = 1,
    int? pageSize = 10);

}
