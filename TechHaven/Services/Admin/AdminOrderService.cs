using Microsoft.EntityFrameworkCore;
using TechHaven.Data;
using TechHaven.DTOs.Public.Order;
using TechHaven.Services.Contracts.Admin;

namespace TechHaven.Services.Admin;

public class AdminOrderService : IAdminOrderService
{
    private readonly ApplicationDbContext _context;

    public AdminOrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OrderListDto>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderListDto(
                o.Id,
                o.Id.ToString().Substring(0, 8),
                o.OrderDate,
                o.OrderItems.Select(oi => new ProductSaleListDto(
                    oi.Product.Name,
                    oi.Product.Price,
                    oi.Quantity,
                    oi.Product.ImageUrl
                )).ToList()
            ))
            .ToListAsync();
    }

    public async Task<OrderListDto?> GetByIdAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null) return null;

        return new OrderListDto(
            order.Id,
            order.Id.ToString().Substring(0, 8),
            order.OrderDate,
            order.OrderItems.Select(oi => new ProductSaleListDto(
                oi.Product.Name,
                oi.Product.Price,
                oi.Quantity,
                oi.Product.ImageUrl
            )).ToList()
        );
    }

    public async Task<bool> CancelOrderAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null) return false;

        foreach (var oi in order.OrderItems)
        {
            var product = await _context.Products.FindAsync(oi.ProductId);
            if (product is not null)
            {
                product.StockQuantity += oi.Quantity;
            }
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}
