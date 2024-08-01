using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShoppingStoreApp.Server.Models;
namespace ShoppingStoreApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product { Id = 12, Name = "Gerbera", Description = "Wedding Flower", Price = 12, Quantity = 7000 });
            products.Add(new Product { Id = 12, Name = "Rose", Description = "Valentine Flower", Price = 24, Quantity = 17000 });
            products.Add(new Product { Id = 12, Name = "Marigold", Description = "Festival Flower", Price =1, Quantity = 56000 });

            return products;

        }

    }
}
