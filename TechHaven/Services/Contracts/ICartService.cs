using TechHaven.DTOs.Cart;

namespace TechHaven.Services.Contracts;

public interface ICartService
{
    Task Add(int productId, int quantity = 1);
    Task Remove(int productId);
    Task UpdateQuantity(int productId, int quantity);
    IEnumerable<CartItemDto> GetCart();
    void Clear();
}
