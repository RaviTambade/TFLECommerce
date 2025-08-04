using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        List<Categories> getAllCategories();
        Categories getCategoryById(int id);
        Categories getCategoryByName(string name);
        bool addNewCategory(Categories category);
        bool updateCategory(Categories category);
        bool deleteCategory(int id);
    }
}