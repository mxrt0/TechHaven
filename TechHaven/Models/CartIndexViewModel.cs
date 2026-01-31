using TechHaven.DTOs.Cart;

namespace TechHaven.Models;

public class CartIndexViewModel
{
    public IEnumerable<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; set; }
}
