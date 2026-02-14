using Microsoft.EntityFrameworkCore;
using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.Data;
using TechHaven.DTOs.Public.Order;
using TechHaven.Services.Contracts.Admin;
using TechHaven.Data.Enums;
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
                o.Status,
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
            order.Id.ToString().Substring(0, 8).ToUpper(),
            order.OrderDate,
            order.Status,
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

        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(IReadOnlyList<OrderListDto>, int totalItems)> SearchAsync(string? searchTerm, OrderSort sort,
        bool showOnlyPending,
        int? page = 1, int? pageSize = 10)
    {
        var orders = _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm)) 
        {
            orders = orders.Where(o => EF.Functions.Like(o.Id.ToString(), searchTerm + "%"));
        }
        orders = sort switch
        {
            OrderSort.Newest => orders.OrderByDescending(o => o.OrderDate),
            OrderSort.Oldest => orders.OrderBy(o => o.OrderDate),
            OrderSort.TotalAsc => orders.OrderBy(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)),
            OrderSort.TotalDesc => orders.OrderByDescending(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)),
            _ => orders
        };
        if (showOnlyPending)
        {
            orders = orders.Where(o => o.Status == OrderStatus.Pending);
        }

        var totalItems = await orders.CountAsync();

        return (await orders
            .Skip(((page ?? 1) - 1) * (pageSize ?? 10))
            .Take(pageSize ?? 10)
            .Select(o => new OrderListDto(
                o.Id,
                o.Id.ToString().Substring(0, 8).ToUpper(),
                o.OrderDate,
                o.Status,
                o.OrderItems.Select(oi => new ProductSaleListDto(
                    oi.Product.Name,
                    oi.Product.Price,
                    oi.Quantity,
                    oi.Product.ImageUrl
                )).ToList()
            ))
            .ToListAsync(), totalItems);
    }

    public async Task<bool> MarkAsShippedAsync(Guid orderId)
    {
        var order = await _context.Orders
            .FindAsync(orderId);

        if (order is null || order.Status != OrderStatus.Pending) return false;
        if (order.OrderItems.Any(oi => oi.Product.StockQuantity < oi.Quantity)) return false;
        order.Status = OrderStatus.Shipped;
        await _context.SaveChangesAsync();
        return true;
    }
}
