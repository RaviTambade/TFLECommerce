using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services.Interfaces;

public interface IAdminProductService
{
    List<AdminProductListItem> GetAll();
    List<AdminProductListItem> GetFiltered(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, bool inStockOnly);
    AdminProductEditViewModel? GetById(int id);
    int Create(AdminProductEditViewModel model, string? imagePath);
    bool Update(AdminProductEditViewModel model, string? imagePath);
    bool Delete(int id);

    List<IdNameOption> GetCategories();
    List<IdNameOption> GetSubcategoriesByCategory(int categoryId);
}

