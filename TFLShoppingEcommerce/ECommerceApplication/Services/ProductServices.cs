using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class ProductServices : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository repo)
        {
            _productRepository = repo;
        }


        public List<Product> getAllProduct()
        {
            List<Product> products = new List<Product>();
            products = _productRepository.getAllProduct();
            return products;
        }
        public Product getProductById(int id)
        {
            Product product = _productRepository.getProductById(id);
            return product;
        }

        public List<Product> getProductByCategoryId(int id)
        {
            List<Product> products = _productRepository.getProductByCategoryId(id);
            return products;
        }

        public Product getProductByTitle(string title)
        {
            Product product = _productRepository.getProductByTitle(title);
            return product;
        }

        public bool addProduct(Product product)
        {
            bool status = false;
            status = _productRepository.addProduct(product);
            return status;
        }

        public bool deleteProduct(int id)
        {
            bool status = false;
            status = _productRepository.deleteProduct(id);
            return status;
        }
        public bool updateProduct(Product product)
        {
            bool status = false;
            status = _productRepository.updateProduct(product);
            return status;
        }
        public List<Product> getCategoriesProduct(int pid)
        {
            List<Product> products = _productRepository.getCategoriesProduct(pid);
            return products;
        }
    }
}