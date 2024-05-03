using Transflower.ECommerce.ProductCatalog.Entities;
using Transflower.ECommerce.ProductCatalog.Services.Interfaces;

namespace Transflower.ECommerce.ProductCatalog.Controllers;
public class ProductsController{

    private IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<Product>> List(){
        return  await  _productService.GetAll();
    }
    public async Task<Product> Details(int id){
        
        return  await _productService.GetById(id);
    }

    public async Task<bool>  Update(Product product){
        return await _productService.Update(product);
    }


    public async Task<bool>  Delete(int id){
        return await _productService.Delete(id);
    }
}