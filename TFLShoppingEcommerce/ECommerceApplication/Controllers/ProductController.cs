using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new List<string>());
            }

            var suggestions = _productService.GetSuggestions(term.Trim(), 5);
            return Json(suggestions);
        }
    }
}
