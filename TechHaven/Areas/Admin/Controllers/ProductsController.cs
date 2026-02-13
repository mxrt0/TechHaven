using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
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

    public async Task<IActionResult> Index(ProductsIndexViewModel filterVm)
    {
        var products = await _productService.SearchAsync(filterVm.SearchTerm, filterVm.CategoryId, filterVm.Stock, filterVm.SortBy);
        var categories = await _categoryService.GetAllAsync();
        ViewData["ActivePage"] = "Products";
        var vm = new ProductsIndexViewModel
        {
            Products = products,
            Categories = categories,
            SearchTerm = filterVm.SearchTerm,
            CategoryId = filterVm.CategoryId,
            Stock = filterVm.Stock,
            SortBy = filterVm.SortBy
        };
        return View(vm);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null) return NotFound();

        var vm = new ProductEditViewModel
        {
            Product = new AdminProductEditDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                ImageUrl = product.ImageUrl,
                IsActive = product.IsActive,
                SpecsJson = product.SpecsJson
            }
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _productService.UpdateAsync(model.Product);
        if (!result)
        {
            return RedirectToAction("Error", "Home", new { area = "", message = "Error updating product data."});
        }
        TempData["SuccessMessage"] = "Product updated successfully.";
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
            return View(vm);
        }

        var result = await _productService.CreateAsync(model.Product);
        if (!result)
        {
            return RedirectToAction("Error", "Home");
        }

        TempData["SuccessMessage"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }
}

