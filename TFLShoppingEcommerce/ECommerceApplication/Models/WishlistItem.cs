namespace ECommerceApplication.Models
{
    public class WishlistItem
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
