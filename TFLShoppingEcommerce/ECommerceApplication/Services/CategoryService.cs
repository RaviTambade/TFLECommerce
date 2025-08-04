using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class CategoryServices : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository repo)
        {
            _categoryRepository = repo;
        }


        public List<Categories> getAllCategories()
        {
            List<Categories> categories = new List<Categories>();
            categories = _categoryRepository.getAllCategories();
            return categories;
        }
        public Categories getCategoryById(int id)
        {
            Categories categories = _categoryRepository.getCategoryById(id);
            return categories;
        }

        public Categories getCategoryByName(string name)
        {
            Categories categories = _categoryRepository.getCategoryByName(name);
            return categories;
        }

        public bool addNewCategory(Categories categories)
        {
            bool status = false;
            status = _categoryRepository.addNewCategory(categories);
            return status;
        }

        public bool deleteCategory(int id)
        {
            bool status = false;
            status = _categoryRepository.deleteCategory(id);
            return status;
        }
        public bool updateCategory(Categories categories)
        {
            bool status = false;
            status = _categoryRepository.updateCategory(categories);
            return status;
        }
    }
}