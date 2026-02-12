using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Services.Contracts.Admin;

public interface IAdminOrderService
{
    Task<IReadOnlyList<OrderListDto>> GetAllAsync();
    Task<IReadOnlyList<OrderListDto>> SearchAsync(
    string? searchTerm,
    OrderSort sort);
    Task<OrderListDto?> GetByIdAsync(Guid orderId);

    Task<bool> CancelOrderAsync(Guid orderId);
}
