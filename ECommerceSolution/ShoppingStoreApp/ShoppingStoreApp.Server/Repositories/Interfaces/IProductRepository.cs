using ShoppingStoreApp.Server.Models;
namespace ShoppingStoreApp.Server.Repositories.Interfaces;

public interface IProductRepository{
    
    
    //Blocking Call

    /* public List<Product> GetAll();
     public Product GetById(int id);
     public bool Insert(Product product);
     public bool Update(Product product);
     public bool Delete(int id);*/


    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int productId);
    Task<bool> Insert(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(int ProductId);
    Task<bool> HikePrice(double percentage);
}

