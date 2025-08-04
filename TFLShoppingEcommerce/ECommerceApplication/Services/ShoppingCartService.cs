using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartrepo;
        public ShoppingCartService(IShoppingCartRepository repo)
        {
            _cartrepo = repo;
        }
        public bool addItem(Item item,int userid)
        {
            bool Status = false;
            Status = _cartrepo.addItem(item, userid);
            return Status;
        }

        public bool deleteItem(int id)
        {
            bool status = _cartrepo.deleteItem(id);
            return status;
        }

        public List<Item> getAllItem(int id)
        {
            List<Item> items = new List<Item>();
            items = (List<Item>)_cartrepo.getAllItem(id);
            return items;
        }

        public List<Product> getItemByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Item getItemById(int id, int userid)
        {
            Item item = _cartrepo.getItemById(id,userid);
            return item;
        }

        public Product getItemByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public bool updateItem(Item item)
        {
            bool status = _cartrepo.updateItem(item);
            return status;
        }
    }
}