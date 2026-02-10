using Microsoft.AspNetCore.Mvc;
using TechHaven.Models;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IWishlistService _wishlistService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    public ProductsController(IProductService productService,
        IWishlistService wishlistService,
        ICartService cartService, ICategoryService categoryService)
    {
        _productService = productService;
        _wishlistService = wishlistService;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(
        string? searchTerm,
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice)
    {
        var products = await _productService.SearchAsync(
        searchTerm,
        categoryId,
        minPrice,
        maxPrice);
        var categories = await _categoryService.GetAllAsync();

        var vm = new ProductsIndexViewModel
        {
            SearchTerm = searchTerm,
            CategoryId = categoryId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Products = products,
            Categories = categories
        };
        return View(vm);
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
