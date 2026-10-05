using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class CatelogController : Controller
{
    private readonly ILogger<CatelogController> _logger;
    IProductService _srv;
    ICategoryService _categorySrv;
    IReviewService _reviewSrv;

    public CatelogController(ILogger<CatelogController> logger, IProductService srv, ICategoryService categorySrv, IReviewService reviewSrv)
    {
        _logger = logger;
        _srv = srv;
        _categorySrv = categorySrv;
        _reviewSrv = reviewSrv;
    }

    public IActionResult Index(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, int? rating)
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            List<ProductSearchResult> products = _srv.GetFilteredProducts(search, categoryId, subcategoryId, minPrice, maxPrice, rating);
            ViewData["allProducts"] = products;
            ViewData["allCategories"] = _categorySrv.getAllCategories();
            ViewData["search"] = search;
            ViewData["categoryId"] = categoryId;
            ViewData["subcategoryId"] = subcategoryId;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["rating"] = rating;
            return View();
        }
    }

    public IActionResult DetailsWithId(int id)
    {
        Product product = _srv.getProductById(id);
        List<Product> recommendations = _srv.getRecommendedProducts(id, 4);
        List<Review> reviews = _reviewSrv.GetReviews(id);
        decimal avgRating = _reviewSrv.GetAverageRating(id);
        ViewData["productById"] = product;
        ViewData["recommendedProducts"] = recommendations;
        ViewData["reviews"] = reviews;
        ViewData["avgRating"] = avgRating;
        return View();
    }

    public IActionResult Details(int id)
    {
        List<Product> products = _srv.getCategoriesProduct(id);
        ViewData["productBycategory"] = products;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
