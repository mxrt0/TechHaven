using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechHaven.Data;
using TechHaven.Data.Models;
using TechHaven.DTOs.Public.Order;
using TechHaven.DTOs.Public.Cart;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Services.Public;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrderService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<bool> CancelOrderAsync(Guid orderId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null || userId is null || order.UserId != userId)
        {
            return false;
        }
        
        var productSales = order.OrderItems.ToList();
        foreach (var ps in productSales)
        {
            var product = await _context.Products.FindAsync(ps.ProductId);
            if (product is not null)
            {
                product.StockQuantity += ps.Quantity;
            }
        };

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateOrderAsync(IEnumerable<CartItemDto> cartItems, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        if (userId is null)
        {
            return false;
        }
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
        };
        var orderItems = cartItems.Select(dto => new OrderItemDto
        (
           dto.ProductId,
           dto.Quantity
        ));
        foreach (var item in orderItems)
        {
            var product = await _context.Products
                .Where(p => p.Id == item.ProductId && p.IsActive)
                .FirstOrDefaultAsync();

            if (product is null || product.StockQuantity < item.Quantity) return false;

            var productSale = new ProductSale
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
            };
            order.OrderItems.Add(productSale);
            product.StockQuantity -= item.Quantity;
        }
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<OrderListDto?> GetOrderByIdAsync(Guid orderId, ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        if (userId is null)
        {
            return null;
        }

        var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null || order.UserId != userId)
        {
            return null;
        }
        var orderDto = new OrderListDto
        (
            OrderId: order.Id,
            OrderNumber: order.Id.ToString().Substring(0, 8).ToUpper(),
            OrderDate: order.OrderDate,
            Status: order.Status,
            OrderItems: order.OrderItems
                        .Where(oi => oi.Product.IsActive)
                        .Select(oi => new ProductSaleListDto
                        (
                            ProductName: oi.Product.Name,
                            UnitPrice: oi.Product.Price,
                            Quantity: oi.Quantity,
                            ImageUrl: oi.Product.ImageUrl
                        )).ToList()
        );
        return orderDto;
    }

    public async Task<IEnumerable<OrderListDto>> GetOrdersByUserIdAsync(ClaimsPrincipal principal)
    {
        var userId = _userManager.GetUserId(principal);
        if (userId is null)
        {
            return Enumerable.Empty<OrderListDto>();
        }
        return await _context.Orders
       .Where(o => o.UserId == userId)
       .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .OrderByDescending(o => o.OrderDate)
       .Select(o => new OrderListDto(
           o.Id,
           o.Id.ToString().Substring(0, 8),
           o.OrderDate,
           o.Status,
           o.OrderItems
           .Where(oi => oi.Product.IsActive)
           .Select(oi => new ProductSaleListDto(
               oi.Product.Name,
               oi.Product.Price,
               oi.Quantity,
               oi.Product.ImageUrl
           )).ToList()
       ))       
       .ToListAsync();
    }
}
