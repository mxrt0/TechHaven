using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechHaven.Data;
using TechHaven.DTOs.Products;
using TechHaven.Services.Contracts;

namespace TechHaven.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;
    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<ProductListDto>> GetAllAsync()
    {
        return await _dbContext
            .Products
             .Include(p => p.Category)
              .AsNoTracking()
               .Select(p => new ProductListDto(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.Category.Name
                    ))
                      .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductListDto>> GetByCategoryAsync(int categoryId)
    {
        return await _dbContext
            .Products
             .Where(p => p.CategoryId == categoryId)
              .Include(p => p.Category)
               .AsNoTracking()
                .Select(p => new ProductListDto(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.Category.Name
                    ))
                      .ToListAsync();
    }

    public async Task<ProductDetailsDto?> GetByIdAsync(int id)
    {
        var product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        return product is null
            ? null
            : new ProductDetailsDto(
                product.Id,
                product.Name,
                product.Description,
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    product.SpecsJson) ?? new(),
                product.Price,
                product.StockQuantity,
                product.ImageUrl,
                product.Category.Name
              );
    }
}
