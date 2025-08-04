namespace ECommerceApplication.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }



        public Categories()
        {
            CategoryId = 0;
            CategoryName = null;
        }

        public Categories(int id, string name)
        {
            CategoryId = id;
            CategoryName = name;
        }
    }
}