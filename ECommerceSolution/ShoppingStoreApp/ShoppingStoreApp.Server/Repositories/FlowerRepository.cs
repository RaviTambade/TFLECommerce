using ShoppingStoreApp.Server.Entities;
using ShoppingStoreApp.Server.Repositories.Interfaces;
namespace ShoppingStoreApp.Server.Repositories;
public class FlowerRepository : IProductRepository
{
    //Memory level CRUD Operations against list of Products

    private List<Product> _products;
    public FlowerRepository(){
        List<Product> products = new List<Product>();
        products.Add(new Product { Title = "Jasmine", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Rose", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Tulip", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Lotus", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Marigold", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        _products=products;
    }
    public async Task<bool> Delete(int id)
    {
        await Task.Delay(5000);
        Product thePRoduct=_products.Find(product => product.Id == id);
        if (thePRoduct != null){
            _products.Remove(thePRoduct);
        }
        return true;
    }

    public async Task<List<Product>> GetAll()
    {
        await Task.Delay(5000);
        return _products;
    }

    public async Task<Product> GetById(int id)
    {
        await Task.Delay(5000);
        return  _products.Find(product => product.Id == id);
    }

    public async Task<bool> Insert(Product product)
    {
        await Task.Delay(5000);
        _products.Add(product);
       return true;
    }

    public async Task<bool> Update(Product product)
    {
        await Task.Delay(5000);
        Product thePRoduct=_products.Find(product => product.Id == product.Id);

        if (thePRoduct != null){
            _products.Remove(thePRoduct);
        }
        _products.Add(product);  
        return true;      
    }
}
