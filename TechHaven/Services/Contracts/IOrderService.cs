using System.Security.Claims;
using TechHaven.DTOs.Order;
using TechHaven.DTOs.Cart;

namespace TechHaven.Services.Contracts;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(IEnumerable<CartItemDto> orderItems, ClaimsPrincipal principal);

    Task<IEnumerable<OrderListDto>> GetOrdersByUserIdAsync(ClaimsPrincipal principal);
    Task<OrderListDto?> GetOrderByIdAsync(Guid orderId, ClaimsPrincipal principal);
    Task<bool> CancelOrderAsync(Guid orderId, ClaimsPrincipal principal);
}