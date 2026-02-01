using Microsoft.AspNetCore.Mvc;
using TechHaven.Models;
using TechHaven.Services.Contracts;

namespace TechHaven.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IWishlistService _wishlistService;
    private readonly ICartService _cartService;
    public ProductsController(IProductService productService, IWishlistService wishlistService, ICartService cartService)
    {
        _productService = productService;
        _wishlistService = wishlistService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Category(int id)
    {
        var products = await _productService.GetByCategoryAsync(id);
        return View(nameof(Index), products);
    }
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        var vm = new DetailsViewModel
        {
            IsInWishlist = await _wishlistService.IsInWishlistAsync(product.Id, User),
            IsInCart = _cartService.IsInCart(product.Id),
            Product = product
        };

        return View(vm);
    }
}
