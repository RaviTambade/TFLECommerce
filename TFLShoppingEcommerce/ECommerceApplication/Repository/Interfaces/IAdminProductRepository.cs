using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Repository.Interfaces;

public interface IAdminProductRepository
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

public class AdminProductListItem
{
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? Image { get; set; }
    public int? CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? SubCategoryName { get; set; }
}

public class IdNameOption
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

