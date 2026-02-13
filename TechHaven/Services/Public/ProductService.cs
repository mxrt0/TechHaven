using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechHaven.Data;
using TechHaven.DTOs.Public.Products;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Services.Public;

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
            .Where(p => p.IsActive)
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
             .Where(p => p.CategoryId == categoryId && p.IsActive)
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
        var product = await _dbContext.Products.
            Where(p => p.IsActive)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

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

    public async Task<(IReadOnlyList<ProductListDto>, int totalItems)> SearchAsync(string? searchTerm, int? categoryId,
        decimal? minPrice, decimal? maxPrice, int? page = 1, int? pageSize = 10)
    {
        var query = _dbContext.Products
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(searchTerm));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

       var totalItems = await query.CountAsync();

        return (await query
             .Skip(((page ?? 1) - 1) * (pageSize ?? 10))
             .Take(pageSize.HasValue ? pageSize.Value : 10)
            .Select(p => new ProductListDto(
                p.Id,
                p.Name,
                p.Price,
                p.ImageUrl,
                p.Category.Name))
            .ToListAsync(), totalItems);

    }

}
