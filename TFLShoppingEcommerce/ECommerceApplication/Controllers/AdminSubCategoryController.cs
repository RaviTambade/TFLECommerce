using ECommerceApplication.Filters;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApplication.Controllers;

[AdminOnly]
public class AdminSubCategoryController : Controller
{
    private readonly IAdminSubcategoryService _subService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public AdminSubCategoryController(IAdminSubcategoryService subService, ICategoryService categoryService, IProductService productService)
    {
        _subService = subService;
        _categoryService = categoryService;
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var subs = _subService.GetAll();
        return View(subs);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(BuildEditVm(new AdminSubcategoryEditViewModel()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminSubcategoryEditViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            ModelState.AddModelError(nameof(model.Name), "Subcategory name is required.");
        }
        if (!model.CategoryId.HasValue)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Category is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(BuildEditVm(model));
        }

        int id = _subService.Create(model.Name, model.CategoryId!.Value, model.ProductId);
        if (id <= 0)
        {
            ModelState.AddModelError(string.Empty, "Failed to create subcategory.");
            return View(BuildEditVm(model));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var sub = _subService.GetById(id);
        if (sub == null) return NotFound();

        var vm = new AdminSubcategoryEditViewModel
        {
            Id = sub.Id,
            Name = sub.Name,
            CategoryId = sub.CategoryId,
            ProductId = sub.ProductId
        };

        return View(BuildEditVm(vm));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AdminSubcategoryEditViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            ModelState.AddModelError(nameof(model.Name), "Subcategory name is required.");
        }
        if (!model.CategoryId.HasValue)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Category is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(BuildEditVm(model));
        }

        bool ok = _subService.Update(model.Id, model.Name, model.CategoryId!.Value, model.ProductId);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Failed to update subcategory.");
            return View(BuildEditVm(model));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        bool ok = _subService.Delete(id);
        if (!ok)
        {
            TempData["SubcategoryDeleteError"] = "Cannot delete subcategory because it is assigned to a product.";
        }
        return RedirectToAction(nameof(Index));
    }

    private AdminSubcategoryEditViewModel BuildEditVm(AdminSubcategoryEditViewModel vm)
    {
        var categories = _categoryService.getAllCategories();
        var products = _productService.getAllProduct();

        vm.Categories = categories
            .Select(c => new SelectListItem(c.CategoryName, c.CategoryId.ToString(), vm.CategoryId.HasValue && vm.CategoryId.Value == c.CategoryId))
            .ToList();
        vm.Categories.Insert(0, new SelectListItem("Select category", "", !vm.CategoryId.HasValue));

        vm.Products = products
            .Select(p => new SelectListItem(p.ProductTitle, p.ProductId.ToString(), vm.ProductId.HasValue && vm.ProductId.Value == p.ProductId))
            .ToList();
        vm.Products.Insert(0, new SelectListItem("No product", "", !vm.ProductId.HasValue));

        return vm;
    }
}

