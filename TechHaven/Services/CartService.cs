using TechHaven.DTOs.Cart;
using TechHaven.Services.Contracts;

namespace TechHaven.Services;

public class CartService : ICartService
{
    public void Add(int productId, int quantity = 1)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CartItemDto> GetCart()
    {
        throw new NotImplementedException();
    }

    public void Remove(int productId)
    {
        throw new NotImplementedException();
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        throw new NotImplementedException();
    }
}
