using Transflower.ECommerce.ProductCatalog.Entities;
using Transflower.ECommerce.ProductCatalog.Repositories.Interfaces;
using Transflower.ECommerce.ProductCatalog.Services.Interfaces;

public class ProductService : IProductService
{
    private IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public  async Task<bool> Delete(int id)
    {
       return await _repository.Delete(id);
    }

    public async Task<List<Product>> GetAll()
    {
       return  await _repository.GetAll();
    }

    public async Task<Product> GetById(int id)
    {   
        return await _repository.GetById(id);   
    }

    public async Task<bool> Insert(Product product)
    {
        return await _repository.Insert(product);
    }

    public async Task<bool> Update(Product product)
    {
        return await _repository.Update(product);
    }
}