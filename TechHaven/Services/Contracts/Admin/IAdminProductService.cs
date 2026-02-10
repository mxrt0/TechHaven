using TechHaven.DTOs.Admin.Products;

namespace TechHaven.Services.Contracts.Admin;

public interface IAdminProductService
{
    Task<IReadOnlyList<AdminProductListDto>> GetAllAsync();
    Task<AdminProductEditDto?> GetByIdAsync(int id);

    Task<bool> CreateAsync(AdminProductCreateDto dto);
    Task<bool> UpdateAsync(AdminProductEditDto dto);
    Task<bool> ToggleActiveAsync(int id);
    Task<bool> DeleteAsync(int id);
}
