using TechHaven.Areas.Admin.ViewModels.Enums;
using TechHaven.DTOs.Admin.Products;
using TechHaven.DTOs.Public.Order;

namespace TechHaven.Services.Contracts.Admin;

public interface IAdminOrderService
{
    Task<IReadOnlyList<OrderListDto>> GetAllAsync();
    Task<(IReadOnlyList<OrderListDto>, int totalItems)> SearchAsync(
    string? searchTerm,
    OrderSort sort,
    int? page = 1,
    int? pageSize = 10);
    Task<OrderListDto?> GetByIdAsync(Guid orderId);

    Task<bool> CancelOrderAsync(Guid orderId);

    Task<bool> MarkAsShippedAsync(Guid orderId);
}
