using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces;

public interface IAdminSubcategoryRepository
{
    List<Subcategory> GetAll();
    List<Subcategory> GetByCategoryId(int categoryId);
    Subcategory? GetById(int id);
    int Create(string name, int categoryId);
    bool Update(int id, string name, int categoryId);
    bool Delete(int id);
}

