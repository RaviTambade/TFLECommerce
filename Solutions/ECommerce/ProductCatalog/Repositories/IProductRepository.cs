using Transflower.ECommerce.ProductCatalog.Entities;
namespace Transflower.ECommerce.ProductCatalog.Repositories.Interfaces;

public interface IProductRepository{
    //Blocking Call

    /* public List<Product> GetAll();
     public Product GetById(int id);
     public bool Insert(Product product);
     public bool Update(Product product);
     public bool Delete(int id);*/


    //Non-Blocking call  : Aysynchronous call
    public   Task<List<Product>> GetAll();
    public   Task<Product> GetById(int id);
    public   Task<bool> Insert(Product product);
    public   Task<bool> Update(Product product);
    public   Task<bool> Delete(int id);
}


//C# 
//Variable:
//Value types Variables
//primitive data types
//      bool , int, float, double, char, 
//      enum, struct
//Reference types Variables
//
//Types:
// Class , Interface, delegate, event