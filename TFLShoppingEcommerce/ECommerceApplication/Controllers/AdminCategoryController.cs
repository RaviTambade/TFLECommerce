using ECommerceApplication.Filters;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers;

[AdminOnly]
public class AdminCategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public AdminCategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _categoryService.getAllCategories();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Categories());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Categories model)
    {
        if (string.IsNullOrWhiteSpace(model.CategoryName))
        {
            ModelState.AddModelError(nameof(model.CategoryName), "Category name is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Existing repository expects an ID; if DB uses AUTO_INCREMENT, pass 0.
        if (model.CategoryId <= 0) model.CategoryId = 0;

        bool ok = _categoryService.addNewCategory(model);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Failed to create category.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var cat = _categoryService.getCategoryById(id);
        if (cat == null) return NotFound();
        return View(cat);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Categories model)
    {
        if (string.IsNullOrWhiteSpace(model.CategoryName))
        {
            ModelState.AddModelError(nameof(model.CategoryName), "Category name is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool ok = _categoryService.updateCategory(model);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Failed to update category.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        // Bonus: “prevent deleting category if subcategories exist” is best enforced by FK constraints.
        // With ON DELETE CASCADE on subcategories, deleting category will delete subcategories.
        // If you want to prevent delete, change FK to RESTRICT and catch the exception here.
        _categoryService.deleteCategory(id);
        return RedirectToAction(nameof(Index));
    }
}

