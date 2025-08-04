using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<Product> getAllProduct();
        Product getProductById(int id);
        List<Product> getProductByCategoryId(int id);
        Product getProductByTitle(string title);
        bool addProduct(Product product);
        bool updateProduct(Product product);
        bool deleteProduct(int id);
         List<Product> getCategoriesProduct(int id);

    }
}