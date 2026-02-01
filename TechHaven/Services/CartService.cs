using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using TechHaven.Data;
using TechHaven.DTOs.Cart;
using TechHaven.Services.Contracts;

namespace TechHaven.Services;

public class CartService : ICartService
{
    private const string CartCookieKey = "th-cart";
    private readonly IHttpContextAccessor _httpContext;
    private readonly ApplicationDbContext _context;
    public CartService(IHttpContextAccessor httpContext, ApplicationDbContext context)
    {
        _httpContext = httpContext;
        _context = context;
    }

    private List<CartItemCookie> ReadCartCookie()
    {
        var cookie = _httpContext.HttpContext?.Request.Cookies[CartCookieKey];

        if (string.IsNullOrEmpty(cookie))
        {
            return new List<CartItemCookie>();
        }

        return JsonConvert.DeserializeObject<List<CartItemCookie>>(cookie) ?? new List<CartItemCookie>();
    }

    private void WriteCartCookie(List<CartItemCookie> cartItems)
    {
        var jsonItems = JsonConvert.SerializeObject(cartItems);
        _httpContext.HttpContext?.Response.Cookies.Append(
            CartCookieKey,
            jsonItems,
            new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                IsEssential = true
            });
    }

    public void Add(int productId, int quantity = 1)
    {
        var cartItems = ReadCartCookie();

        var item = cartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (quantity <= 0)
        {
            return;
        }
        if (item is not null)
        {
            item.Quantity += quantity;
        }
        else
        {
            var newItem = new CartItemCookie(productId, quantity);
            cartItems.Add(newItem);
        }

        WriteCartCookie(cartItems);
    }

    public void Clear() => _httpContext.HttpContext?.Response.Cookies.Delete(CartCookieKey);

    public IEnumerable<CartItemDto>? GetCart()
    {
        var cookieItems = ReadCartCookie();

        var productIds = cookieItems.Select(ci => ci.ProductId).ToList();

        var products = _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionary(p => p.Id, p => p);

        var cartItems = cookieItems
            .Where(cookieItems => products.ContainsKey(cookieItems.ProductId))
            .Select(i => new CartItemDto
            (
                ProductId: i.ProductId,
                Name: products[i.ProductId].Name,
                Price: products[i.ProductId].Price,                
                Total: products[i.ProductId].Price * i.Quantity,
                Quantity: i.Quantity
            ));

        return cartItems;
    }

    public void Remove(int productId)
    {
        var cart = ReadCartCookie();
        cart.RemoveAll(i => i.ProductId == productId);
        WriteCartCookie(cart);
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = ReadCartCookie();
        var item = cart.FirstOrDefault(i => i.ProductId == productId);

        if (item is null) return;

        if (quantity <= 0)
        {
            cart.Remove(item);
        }
        else
        {
            item.Quantity = quantity;

        }

        WriteCartCookie(cart);
    }

    public bool IsInCart(int productId)
    {
        var cart = ReadCartCookie();
        return cart.Any(i => i.ProductId == productId);
    }
}
