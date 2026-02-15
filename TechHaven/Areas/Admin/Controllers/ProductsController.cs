using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using TechHaven.Areas.Admin.ViewModels;
using TechHaven.Common;
using TechHaven.DTOs.Admin.Products;
using TechHaven.Services.Contracts.Admin;
using TechHaven.Services.Contracts.Public;

namespace TechHaven.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private const int PageSize = 10;
    private readonly IAdminProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IAdminProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(ProductsIndexViewModel filterVm, int page = 1)
    {
        if (page < 1) page = 1;
        var (products, totalItems) = await _productService.SearchAsync(filterVm.SearchTerm, filterVm.CategoryId,
            filterVm.Stock, filterVm.SortBy,
            page, PageSize);
        var categories = await _categoryService.GetAllAsync();
        ViewData["ActivePage"] = "Products";
        var vm = new ProductsIndexViewModel
        {
            Products = products,
            Categories = categories,
            SearchTerm = filterVm.SearchTerm,
            CategoryId = filterVm.CategoryId,
            Stock = filterVm.Stock,
            SortBy = filterVm.SortBy,
            Page = page,
            PageSize = ProductsController.PageSize,
            TotalItems = totalItems
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
            TempData["Admin_ErrorMessage"] = Messages.ErrorUpdatingProductMessage;
            return RedirectToAction(nameof(Index));
        }
        TempData["Admin_SuccessMessage"] = Messages.ProductUpdatedMessage;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var result = await _productService.ToggleActiveAsync(id);
        if (!result)
        {
            TempData["Admin_ErrorMessage"] = Messages.ErrorTogglingProductActiveMessage;
            return RedirectToAction(nameof(Index));
        }
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
            model.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", model.Product.CategoryId);
            return View(model);
        }

        var result = await _productService.CreateAsync(model.Product);
        if (!result)
        {
            TempData["Admin_ErrorMessage"] = Messages.ErrorCreatingProductMessage;
            return RedirectToAction(nameof(Index));
        }

        TempData["Admin_SuccessMessage"] = Messages.ProductCreatedMessage;
        return RedirectToAction(nameof(Index));
    }
}

