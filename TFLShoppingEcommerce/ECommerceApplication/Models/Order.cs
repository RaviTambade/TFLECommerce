namespace ECommerceApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly orderDate { get; set; }
        public string ShippingAddress { get; set; }
        public double TotalAmount { get; set; }
        public DateOnly? ShippingDate { get; set; }
        public string status { get; set; }
        public string Address{ get; set; }
        public string? CouponCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
    }
}
