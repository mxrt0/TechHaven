using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechHaven.Areas.Admin.ViewModels;
using TechHaven.DTOs.Admin.Products;
using TechHaven.Services.Contracts.Admin;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly IAdminProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IAdminProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        ViewData["ActivePage"] = "Products";
        var vm = new ProductsIndexViewModel
        {
            Products = products
        };
        return View(vm);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null) return NotFound();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AdminProductEditDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        await _productService.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleActive(int id)
    {
        await _productService.ToggleActiveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ProductCreateViewModel
        {
            Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name")                       
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (!ModelState.IsValid) 
        {
            var vm = new ProductCreateViewModel
            {
                Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", selectedValue: model.Product.CategoryId)
            }
        ;
            var errors = ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value?.Errors });

            foreach (var e in errors)
            {
                Console.WriteLine($"{e.Key}: {string.Join(", ", e.Errors?.Select(er => er.ErrorMessage) ?? Enumerable.Empty<string>())}");
            }
            return View(vm);
        }

        var result = await _productService.CreateAsync(model.Product);
        if (!result)
        {
            return RedirectToAction("Error", "Home");
        }
        return RedirectToAction(nameof(Index));
    }
}

