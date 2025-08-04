namespace ECommerceApplication.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public Product product { get; set; }
        public int Quantity{ get; set; }
    
        public Item()
        {
            ItemId = 0;
            product = null;
            Quantity = 0;
        }

        public Item( Product p, int quantity)
        {
            product = p;
            Quantity = quantity;
        }
        public Item(int id, Product p, int quantity)
        {
            ItemId = id;
            product = p;
            Quantity = quantity;
        }
    }
}