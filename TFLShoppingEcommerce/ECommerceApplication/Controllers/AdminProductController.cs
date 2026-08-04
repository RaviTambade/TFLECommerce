using ECommerceApplication.Filters;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApplication.Controllers;

[AdminOnly]
public class AdminProductController : Controller
{
    private readonly ILogger<AdminProductController> _logger;
    private readonly IAdminProductService _service;
    private readonly IWebHostEnvironment _env;

    public AdminProductController(
        ILogger<AdminProductController> logger,
        IAdminProductService service,
        IWebHostEnvironment env)
    {
        _logger = logger;
        _service = service;
        _env = env;
    }

    [HttpGet]
    public IActionResult Index(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, bool inStockOnly = false)
    {
        var categories = _service
            .GetCategories()
            .Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoryId.HasValue && c.Id == categoryId.Value))
            .ToList();

        categories.Insert(0, new SelectListItem("All categories", "", !categoryId.HasValue));

        var subcategories = new List<SelectListItem> { new SelectListItem("All subcategories", "", !subcategoryId.HasValue) };
        if (categoryId.HasValue)
        {
            subcategories.AddRange(_service
                .GetSubcategoriesByCategory(categoryId.Value)
                .Select(s => new SelectListItem(s.Name, s.Id.ToString(), subcategoryId.HasValue && s.Id == subcategoryId.Value)));
        }

        var products = _service.GetFiltered(search, categoryId, subcategoryId, minPrice, maxPrice, inStockOnly);

        var vm = new AdminProductIndexViewModel
        {
            Search = search,
            CategoryId = categoryId,
            SubcategoryId = subcategoryId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            Categories = categories,
            Subcategories = subcategories,
            Products = products
        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = _service.GetCategories() ?? new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
        ViewBag.Subcategories = new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
        return View(new AdminProductEditViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminProductEditViewModel model, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _service.GetCategories() ?? new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
            ViewBag.Subcategories = model.CategoryId.HasValue
                ? _service.GetSubcategoriesByCategory(model.CategoryId.Value)
                : new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
            return View(model);
        }

        string? imagePath = SaveImageIfAny(imageFile);
        int id = _service.Create(model, imagePath);
        if (id <= 0)
        {
            ModelState.AddModelError(string.Empty, "Failed to create product.");
            ViewBag.Categories = _service.GetCategories() ?? new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
            ViewBag.Subcategories = model.CategoryId.HasValue
                ? _service.GetSubcategoriesByCategory(model.CategoryId.Value)
                : new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _service.GetById(id);
        if (product == null) return NotFound();
        ViewBag.Categories = _service.GetCategories();
        ViewBag.Subcategories = product.CategoryId.HasValue
            ? _service.GetSubcategoriesByCategory(product.CategoryId.Value)
            : new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AdminProductEditViewModel model, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _service.GetCategories();
            ViewBag.Subcategories = model.CategoryId.HasValue
                ? _service.GetSubcategoriesByCategory(model.CategoryId.Value)
                : new List<ECommerceApplication.Repository.Interfaces.IdNameOption>();
            return View(model);
        }

        string? imagePath = SaveImageIfAny(imageFile);
        bool ok = _service.Update(model, imagePath);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Failed to update product.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult GetSubcategoriesByCategory(int categoryId)
    {
        var subs = _service.GetSubcategoriesByCategory(categoryId)
            .Select(s => new { id = s.Id, name = s.Name })
            .ToList();
        return Json(subs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private string? SaveImageIfAny(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0) return null;

        // Very basic content-type check (still validate on upload in real apps)
        if (!imageFile.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        string uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "products");
        Directory.CreateDirectory(uploadsDir);

        string ext = Path.GetExtension(imageFile.FileName);
        if (string.IsNullOrWhiteSpace(ext)) ext = ".jpg";

        string fileName = $"{Guid.NewGuid():N}{ext}";
        string fullPath = Path.Combine(uploadsDir, fileName);

        using (var stream = System.IO.File.Create(fullPath))
        {
            imageFile.CopyTo(stream);
        }

        // store as web path
        return $"/uploads/products/{fileName}";
    }
}

