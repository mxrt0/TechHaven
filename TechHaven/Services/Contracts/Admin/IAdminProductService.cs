using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Products;
using TechHaven.Areas.Admin.ViewModels.Enums;
namespace TechHaven.Services.Contracts.Admin;

public interface IAdminProductService
{
    Task<IReadOnlyList<AdminProductListDto>> GetAllAsync();
    Task<AdminProductEditDto?> GetByIdAsync(int id);
    Task<IReadOnlyList<AdminProductListDto>> SearchAsync(
    string? searchTerm,
    int? categoryId,
    StockFilter stockFilter,
    ProductSort sortBy);
    Task<bool> CreateAsync(AdminProductCreateDto dto);
    Task<bool> UpdateAsync(AdminProductEditDto dto);
    Task<bool> ToggleActiveAsync(int id);
    Task<bool> DeleteAsync(int id);
}
