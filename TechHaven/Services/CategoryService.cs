using Microsoft.EntityFrameworkCore;
using TechHaven.Data;
using TechHaven.DTOs.Categories;
using TechHaven.Services.Contracts;

namespace TechHaven.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext;
    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<CategoryListDto>> GetAllAsync()
    {
        return await _dbContext.Categories
             .AsNoTracking()
              .Select(c => new CategoryListDto(
                  c.Id,
                  c.Name,
                  c.ImageUrl
              ))
               .ToListAsync();
    }

    public async Task<CategoryListDto?> GetByIdAsync(int id)
    {
        return await _dbContext.Categories
            .AsNoTracking()
             .Where(c => c.Id == id)
              .Select(c => new CategoryListDto(
                c.Id,
                c.Name,
                c.ImageUrl
              ))
               .FirstOrDefaultAsync();
    }
}
