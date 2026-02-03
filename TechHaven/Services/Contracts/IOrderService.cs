using System.Security.Claims;
using TechHaven.Data.Models;
using TechHaven.DTOs.Order;

namespace TechHaven.Services.Contracts;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(IEnumerable<OrderItemDto> orderItems, ClaimsPrincipal principal);

    Task<IEnumerable<OrderListDto>> GetOrdersByUserIdAsync(ClaimsPrincipal principal);
    Task<OrderListDto?> GetOrderByIdAsync(Guid orderId);
    Task<bool> CancelOrderAsync(Guid orderId, ClaimsPrincipal principal);
}