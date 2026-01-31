using TechHaven.DTOs.Cart;

namespace TechHaven.Services.Contracts;

public interface ICartService
{
    void Add(int productId, int quantity = 1);
    void Remove(int productId);
    void UpdateQuantity(int productId, int quantity);
    IEnumerable<CartItemDto> GetCart();
    void Clear();
}
