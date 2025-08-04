using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IShoppingCartRepository
    {
        List<Item> getAllItem(int id);
        Item getItemById(int id,int userid);
        List<Product> getItemByCategoryId(int id);
        Product getItemByTitle(string title);
        bool addItem(Item item,int userid);
        bool updateItem(Item item);
        bool deleteItem(int id);
    }
}