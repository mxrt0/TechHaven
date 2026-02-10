using System.Security.Claims;
using TechHaven.DTOs.Public.Cart;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Services.Contracts.Public;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(IEnumerable<CartItemDto> orderItems, ClaimsPrincipal principal);

    Task<IEnumerable<OrderListDto>> GetOrdersByUserIdAsync(ClaimsPrincipal principal);
    Task<OrderListDto?> GetOrderByIdAsync(Guid orderId, ClaimsPrincipal principal);
    Task<bool> CancelOrderAsync(Guid orderId, ClaimsPrincipal principal);
}