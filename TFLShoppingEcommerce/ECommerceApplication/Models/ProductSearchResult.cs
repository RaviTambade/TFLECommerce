namespace ECommerceApplication.Models
{
    public class ProductSearchResult
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public decimal AvgRating { get; set; }
    }
}
