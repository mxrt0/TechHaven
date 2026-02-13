using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Products;
using TechHaven.Areas.Admin.ViewModels.Enums;
namespace TechHaven.Services.Contracts.Admin;

public interface IAdminProductService
{
    Task<IReadOnlyList<AdminProductListDto>> GetAllAsync();
    Task<AdminProductEditDto?> GetByIdAsync(int id);
    Task<(IReadOnlyList<AdminProductListDto>, int totalItems)> SearchAsync(
    string? searchTerm,
    int? categoryId,
    StockFilter stockFilter,
    ProductSort sortBy,
    int? page = 1,
    int? pageSize = 10);
    Task<bool> CreateAsync(AdminProductCreateDto dto);
    Task<bool> UpdateAsync(AdminProductEditDto dto);
    Task<bool> ToggleActiveAsync(int id);
    Task<bool> DeleteAsync(int id);
}
