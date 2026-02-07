using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechHaven.Data;
using TechHaven.Data.Models;
using TechHaven.DTOs.Admin.Products;
using TechHaven.Services.Contracts.Admin;

namespace TechHaven.Services.Admin;

public class AdminProductService : IAdminProductService
{
    private readonly ApplicationDbContext _context;

    public AdminProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AdminProductListDto>> GetAllAsync()
        => await _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .Select(p => new AdminProductListDto(
                p.Id,
                p.Name,
                p.Price,
                p.StockQuantity,
                p.IsActive,
                p.Category.Name
            ))
            .ToListAsync();

    public async Task<AdminProductEditDto?> GetByIdAsync(int id)
        => await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new AdminProductEditDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive
            })
            .FirstOrDefaultAsync();

    public async Task<bool> CreateAsync(AdminProductCreateDto dto)
    {
        try
        {
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category is null) return false;

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                SpecsJson = JsonConvert.SerializeObject(dto.SpecsJson) ?? "{}",
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
       
        return true;
    }

    public async Task<bool> UpdateAsync(AdminProductEditDto dto)
    {
        var product = await _context.Products.FindAsync(dto.Id);
        if (product is null) return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;
        product.ImageUrl = dto.ImageUrl;
        product.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        product.IsActive = !product.IsActive;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        product.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
