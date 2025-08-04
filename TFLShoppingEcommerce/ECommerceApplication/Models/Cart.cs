namespace ECommerceApplication.Models
{
    public class Cart
    {
        public List<Item> items = new List<Item>();

        public void addToCart(Item item)
        {
            items.Add(item);
        }
        public void removeFromCart(Item item)
        {
            items.Remove(item);
         }
       
    }
}