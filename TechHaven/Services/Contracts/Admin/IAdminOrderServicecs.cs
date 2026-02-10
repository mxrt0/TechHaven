using TechHaven.DTOs.Public.Order;

namespace TechHaven.Services.Contracts.Admin;

public interface IAdminOrderService
{
    Task<IReadOnlyList<OrderListDto>> GetAllAsync();
    Task<OrderListDto?> GetByIdAsync(Guid orderId);

    Task<bool> CancelOrderAsync(Guid orderId);
}
