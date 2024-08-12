using ShoppingStoreApp.Server.Models;
using ShoppingStoreApp.Server.Repositories.Interfaces;
using ShoppingStoreApp.Server.Services.Interfaces;

public class ProductService : IProductService
{
    private IProductRepository _repository;

    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = _repo.GetAll();
        Console.WriteLine("service");
        return await products;
    }
    public async Task<Product> GetById(int productId) => await _repo.GetById(productId);
    public async Task<bool> Insert(Product product) => await _repo.Insert(product);
    public async Task<bool> Update(Product product) => await _repo.Update(product);
    public async Task<bool> Delete(int productId) => await _repo.Delete(productId);
    public async Task<bool> HikePrice(double percentage) => await _repo.HikePrice(percentage);

}