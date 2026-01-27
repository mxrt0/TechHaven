using Microsoft.AspNetCore.Mvc;
using TechHaven.Services.Contracts;

namespace TechHaven.Controllers;

//TODO: Commit Atomically To Git With Appropriate Messages
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
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
        return product is null ? NotFound() : View(product);
    }
}
