using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services;

public class AdminProductService : IAdminProductService
{
    private readonly IAdminProductRepository _repo;

    public AdminProductService(IAdminProductRepository repo)
    {
        _repo = repo;
    }

    public List<AdminProductListItem> GetAll() => _repo.GetAll();

    public List<AdminProductListItem> GetFiltered(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, bool inStockOnly)
        => _repo.GetFiltered(search, categoryId, subcategoryId, minPrice, maxPrice, inStockOnly);

    public AdminProductEditViewModel? GetById(int id) => _repo.GetById(id);

    public int Create(AdminProductEditViewModel model, string? imagePath) => _repo.Create(model, imagePath);

    public bool Update(AdminProductEditViewModel model, string? imagePath) => _repo.Update(model, imagePath);

    public bool Delete(int id) => _repo.Delete(id);

    public List<IdNameOption> GetCategories() => _repo.GetCategories();

    public List<IdNameOption> GetSubcategoriesByCategory(int categoryId) => _repo.GetSubcategoriesByCategory(categoryId);
}

