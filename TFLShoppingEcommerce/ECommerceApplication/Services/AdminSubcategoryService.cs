using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services;

public class AdminSubcategoryService : IAdminSubcategoryService
{
    private readonly IAdminSubcategoryRepository _repo;

    public AdminSubcategoryService(IAdminSubcategoryRepository repo)
    {
        _repo = repo;
    }

    public List<Subcategory> GetAll() => _repo.GetAll();
    public List<Subcategory> GetByCategoryId(int categoryId) => _repo.GetByCategoryId(categoryId);
    public Subcategory? GetById(int id) => _repo.GetById(id);
    public int Create(string name, int categoryId) => _repo.Create(name, categoryId);
    public bool Update(int id, string name, int categoryId) => _repo.Update(id, name, categoryId);
    public bool Delete(int id) => _repo.Delete(id);
}

