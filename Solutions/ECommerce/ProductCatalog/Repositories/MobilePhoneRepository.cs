using Transflower.ECommerce.ProductCatalog.Entities;
using Transflower.ECommerce.ProductCatalog.Repositories.Interfaces;
namespace Transflower.ECommerce.ProductCatalog.Repositories;
public class MobilePhoneRepository : IProductRepository
{
    //Memory level CRUD Operations against list of Products
    private List<Product> _products;
    public MobilePhoneRepository(){
        List<Product> products = new List<Product>();
        
        //Get data from producs.json file
        //Create products list using Deserialization concept
        //return products

        products.Add(new Product { Title = "Motog", Description = "Smart Phone Motorola", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "iPhone10", Description = "Best Phone Apple", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Galaxy", Description = "Professional Phone Samsung", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "OnePlus", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        products.Add(new Product { Title = "Realmee", Description = "Smelling Flower", Category = "flowers", StockAvailable = 4500, UnitPrice = 12 });
        _products=products;
    }

    //Asynchronous methods
    public async Task<bool> Delete(int id)
    {
        //Product theProduct = null;
        //Arrow Function ( lambda)
        await Task.Delay(1000);
        Product theProduct=_products.Find(product => product.Id == id);
        /*foreach (Product p in _products)
        {   if (p.Id == id)
            {
                theProduct = p;
            }
        }
        */
        if (theProduct != null)
        {
            _products.Remove(theProduct);
        }
        return true;
    }
    public async Task<List<Product>> GetAll()
    {
        await Task.Delay(1000);
        return _products;
    }

    public async Task<Product> GetById(int id)
    {
        await Task.Delay(1000);
        return  _products.Find(product => product.Id == id);
    }

    public async Task<bool> Insert(Product product)
    {
        await Task.Delay(1000);
        _products.Add(product);
       return true;
    }

    public async Task<bool> Update(Product product)
    {
        await Task.Delay(1000);
        Product thePRoduct=_products.Find(product => product.Id == product.Id);
        if (thePRoduct != null){
            _products.Remove(thePRoduct);
        }
        _products.Add(product);  
        return true;      
    }
}
