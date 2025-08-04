namespace ECommerceApplication.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string ProductImage { get; set; }


        public Product()
        {
            ProductId = 0;
            ProductTitle = null;
            Description = null;
            Quantity = 0;
            UnitPrice = 0;
            ProductImage = null;
        }

        public Product(int id, string title, string description, int quantity, double unitPrice, string imagePath)
        {
            ProductId = id;
            ProductTitle = title;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductImage = imagePath;
        }
    }
}